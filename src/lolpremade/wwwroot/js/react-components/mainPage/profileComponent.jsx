import Modal from 'react-modal';

const imgStyle = {
    height: '50px',
    width: '50px'
};

class TeamInviteMessage extends React.Component {
    constructor(props) {
        super(props);
        this.state = { teamname: '' }

        this.getTeamName = this.getTeamName.bind(this);
        this.componentDidMount = this.componentDidMount.bind(this);
        this.onAccept = this.onAccept.bind(this);
        this.onRefuse = this.onRefuse.bind(this);
    }
    getTeamName() {
        var apiAccess = new APIAccess();
        return apiAccess.GetById('teams', this.props.teamid).then(function (result) {
            return result.name
        })
    }
    componentDidMount() {
        this.getTeamName().then(function (result) {
            this.setState({
                teamname: result
            });
        }.bind(this));
    }
    onAccept() {
        var apiService = new APIAccess();
        var acceptedInvitation = this.props.invite;
        acceptedInvitation.accepted = true;
        apiService.Put('teaminvites', acceptedInvitation.id, JSON.stringify(acceptedInvitation), localStorage.getItem('token')).then(function (result) {
            if (result.ok)
            {
                alert('Invite for team Accepted');
                this.props.deleteInvite(this.props.invite.id);
            }
                
        }.bind(this));
    }
    onRefuse() {
        var apiService = new APIAccess();
        apiService.Delete('teaminvites', this.props.invite.id, localStorage.getItem('token')).then(function (result) {
            if (result.ok)
            {
                alert('Invite refused');
                this.props.deleteInvite(this.props.invite.id);
            }
        }.bind(this))
    }
    render() {
        return <div className="row">
                    <span>Invitación de equipo: {this.state.teamname}</span>
                    <div role="group" className="btn-group pull-right">
                        <button className="btn btn-default" type="button" onClick={this.onAccept}>Aceptar</button>
                        <button className="btn btn-default" type="button" onClick={this.onRefuse}>Rechazar</button>
                    </div>
               </div>
    }
}

class MessageComponent extends React.Component {
    constructor() {
        super();
        this.state = { showMessage: false, senderUser: {} }

        this.getSenderUser = this.getSenderUser.bind(this);
        this.showMessage = this.showMessage.bind(this);
        this.onCloseMessage = this.onCloseMessage.bind(this);
    }
    getSenderUser() {
        var apiService = new APIAccess();
        apiService.GetById('users',this.props.message.senderUserId).then(function(result) {
            if(result != null) {
                this.setState({
                    senderUser: result.username
                })
            }
        })
    }
    showMessage() {
        this.setState({
            showMessage: true
        })
    }
    onCloseMessage() {
        this.setState({
            showMessage: false
        })
    }
    render() {
        return <div className="inline">
                <Modal isOpen={this.state.showMessage}
                       onRequestClose={this.onCloseMessage}
                       contentLabel="Example Modal">
                                   
                       <form>
                            <div className="form-group">
                                <label className="control-label">Message Subject: { this.props.message.subject }</label>
                                <label>From: { this.state.senderUser.username }</label>
                            </div>
                            <div className="form-group">
                                <p>{ this.props.message.messageText }</p>
                                <label>{ this.props.message.messageDate }</label>
                            </div>
                       </form>
                </Modal>
                <label>Message subject: { this.props.message.subject }</label>
                <button className="btn" onClick={ this.showMessage }>Open message</button>
              </div>
    }
}

class MessagesComponent extends React.Component {
    constructor() {
        super();
        this.state = { messages: [], teaminvites: [] }

        this.getMessages = this.getMessages.bind(this);
        this.getUserTeamInvites = this.getUserTeamInvites.bind(this);
        this.componentDidMount = this.componentDidMount.bind(this);
        this.deleteInvite = this.deleteInvite.bind(this);
    }
    getMessages() {
        var apiAccess = new APIAccess();
        return apiAccess.GetById('users',localStorage.getItem('username')).then(function(result) {
            if(result != null) {
                return apiAccess.GetById('messages',result.id).then(function(result) {
                    return result;
                }.bind(this))
            }
        }.bind(this))
    }
    getUserTeamInvites() {
        var apiAccess = new APIAccess();
        return apiAccess.GetById('teaminvites', localStorage.getItem('username')).then(function (result) {
            return result;
        })
    }
    componentDidMount() {
        this.getUserTeamInvites().then(function (result) {
            this.setState({
                teaminvites: result
            })
        }.bind(this)).then(function() {
            this.getMessages().then(function (result) {
                this.setState({
                    messages: result
                })
            }.bind(this))
        }.bind(this))
    }
    deleteInvite(id) {
        this.setState({
            teaminvites: this.state.messages.filter(function (obj) {
                return obj.id != id
            })
        })
    }
    render() {
        var invitesComponents = []
        var messageComponents = []
        if (this.state.teaminvites.length > 0) {
            for(var i = 0;i<this.state.teaminvites.length;i++) {
                var currentInvite = this.state.teaminvites[i];
                if(!currentInvite.accepted)
                    invitesComponents.push(<TeamInviteMessage key={i} teamid={currentInvite.senderTeamId } invite={currentInvite} deleteInvite={ this.deleteInvite}/>);
            }
        }
        if (this.state.messages.length > 0) {
            for(var i=0;i<this.state.messages.length;i++) {
                var currentMessage = this.state.messages[i];
                messageComponents.push(<MessageComponent key={i} message={currentMessage} />)
            }
        }

        return <div>
                    <div className="panel panel-default">
                        <div className="panel-heading">
                            <h3 className="panel-title">Messages </h3>
                        </div>
                        <div className="panel-body">
                            {invitesComponents}
                            {messageComponents}
                        </div>
                    </div>
               </div>
    }
}

class ProfilePage extends React.Component {
    constructor() {
        super();
        this.state = { user: '', syncModalIsOpen: false, lolusername: '', resultModalIsOpen: false, synckey: '', editUser: false, useravatar: '' }
        
        this.getUserInfo = this.getUserInfo.bind(this)
        this.componentDidMount = this.componentDidMount.bind(this)
        this.onSyncModalOpen = this.onSyncModalOpen.bind(this)
        this.onLolUsernameChanged = this.onLolUsernameChanged.bind(this)
        this.closeModal = this.closeModal.bind(this)
        this.onVerifyAccountClicked = this.onVerifyAccountClicked.bind(this)
        this.onAlreadyVerifiedIt = this.onAlreadyVerifiedIt.bind(this)
        this.onEditUserClicked = this.onEditUserClicked.bind(this)
        this.onHideEditableClicked = this.onHideEditableClicked.bind(this)
    }
    getUserInfo() {
        var apiAccess = new APIAccess();
        return apiAccess.GetById('users', localStorage.getItem('username')).then(function (result) {
            return result;
        })
    }
    componentDidMount() {
        this.getUserInfo().then(function (result) {
            this.setState({
                user: result,
                useravatar: "images/users/" + result.username + "avatar.jpeg"
            })
        }.bind(this))
    }
    onLolUsernameChanged(e) {
        this.setState({
            lolusername: e.target.value
        })
    }
    onSyncModalOpen() {
        this.setState({syncModalIsOpen: true});
    }
    closeModal() {
        this.setState({syncModalIsOpen: false, resultModalIsOpen: false});
    }
    onVerifyAccountClicked() {
        if(this.state.lolusername == '') {
            alert('Error, the username is empty')
        }
        else {
            var apiService = new APIAccess();
            var request = {
                username: localStorage.getItem('username'),
                lolusername: this.state.lolusername
            }
            apiService.PostAuthorized('lolusersinfo',JSON.stringify(request),localStorage.getItem('token')).then(function(result){
                return result.json();
            }.bind(this)).then(function(resultBody){
                this.setState({
                    synckey: resultBody.key,
                    syncModalIsOpen: false,
                    resultModalIsOpen: true
                })
            }.bind(this))
        }
    }
    onAlreadyVerifiedIt() {
        var apiService = new APIAccess();
        if(this.state.lolusername == '') {
            alert('Error, the username is empty')
        }
        else
        {
            var request = {
                username: localStorage.getItem('username'),
                lolusername: this.state.lolusername
            }
            var queryoptions = {
                authorize: true
            }
            apiService.Put('lolusersinfo','',JSON.stringify(request),localStorage.getItem('token'),queryoptions).then(function(result){
                return result.json();
            }.bind(this)).then(function(result){
                if(result.response == "Succesful authentication") {
                    alert('Account syncronized');
                    this.getUserInfo().then(function (result) {
                        this.setState({
                            user: result
                        })
                    }.bind(this))
                }
                else {
                    alert('Error authenticating, please insert the code as a runes page');
                }
            }.bind(this)) 
        }
    }
    onEditUserClicked() {
        this.setState({
            editUser: true
        })
    }
    onHideEditableClicked() {
        this.setState({
            editUser: false
        })
    }
    render() {
        if(this.state.editUser) {
            return <ProfilePageEditable user={ this.state.user } hideEditable={ this.onHideEditableClicked } />
        }
        else {
            var date = this.state.user.dateOfBirth;
            if (this.state.user.dateOfBirth == '0001-01-01T00:00:00') {
                date = '';
            }
            return <div>
                        <div className="col-md-12">
                            <Modal isOpen={this.state.syncModalIsOpen}
                                   onRequestClose={this.closeModal}
                                   contentLabel="Example Modal">
                                   
                                  <form>
                                      <div className="form-group">
                                        <label className="control-label">League of Legends Username: </label>
                                        <input type="text" className="form-control" onChange={ this.onLolUsernameChanged } />
                                      </div>
                                      <div className="form-group">
                                        <input className="btn btn-primary btn-block" type="button" value="Verify Your Account" onClick={ this.onVerifyAccountClicked } />
                                        <input className="btn btn-primary btn-block" type="button" value="I have inserted the code already" onClick={ this.onAlreadyVerifiedIt } />
                                      </div>
                                  </form>
                            </Modal>
                            <Modal isOpen={this.state.resultModalIsOpen}
                                   onRequestClose={this.closeModal}
                                   contentLabel="Example Modal">

                                   <p>Log In into the League of Legends client and create
                                        a Mastery Page with this code as a name so we can
                                        verify that the account is yours</p>
                                   <label>Code: { this.state.synckey }</label>
                            </Modal>
                            <div className="panel panel-default">
                                <div className="panel-body">
                                    <div className="media">
                                        <div className="media-left">
                                            <a><img src={ this.state.user.userAvatar } className="img-circle" style={imgStyle} /></a>
                                        </div>
                                        <div className="media-body">
                                            <ul className="list-unstyled fa-ul">
                                                <li><i className="fa fa-user fa-li"></i>{this.state.user.username}</li>
                                                <li><span>Role: { this.state.user.role }</span></li>
                                                <li><span>Rank: { this.state.user.rank }</span></li>
                                                <li><span>Region { this.state.user.playRegion }</span></li>
                                                <li><span>Language { this.state.user.language }</span></li>
                                                <li><span>Country { this.state.user.country }</span></li>
                                                <li><span>Birthdate { date }</span></li>
                                            </ul>
                                        </div>
                                    </div>
                                    <hr />
                                    <div className="row">
                                        <button className="btn btn-primary" onClick={ this.onSyncModalOpen }>Synchronize League of Legends Account</button>
                                        <button className="btn btn-primary" onClick={this.onEditUserClicked}>Edit Information</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <MessagesComponent />
                   </div>
        }
    }
}

class ProfilePageEditable extends React.Component {
    constructor() {
        super();
        this.state = { region: '', country: '', language: '',role: '', dob: '',avatar: '', useravatar: '' }

        this.onChangeRegion = this.onChangeRegion.bind(this)
        this.onChangeCountry = this.onChangeCountry.bind(this)
        this.onChangeLanguage = this.onChangeLanguage.bind(this)
        this.onChangeRole = this.onChangeRole.bind(this)
        this.onChangeDateOfBirth = this.onChangeDateOfBirth.bind(this)
        this.onUpdateUser = this.onUpdateUser.bind(this);
        this.onAvatarChanged = this.onAvatarChanged.bind(this);
        this.onUploadAvatar = this.onUploadAvatar.bind(this);
    }
    componentDidMount() {
        this.setState({
            useravatar: "images/users/" + this.props.user.username + "avatar.jpeg"
        })
    }
    onChangeRegion(e) {
        this.setState({
            region: e.target.value
        })
    }
    onChangeCountry(e) {
        this.setState({
            country: e.target.value
        })
    }
    onChangeLanguage(e) {
        this.setState({
            language: e.target.value
        })
    }
    onChangeRole(e) {
        this.setState({
            role: e.target.value
        })
    }
    onChangeDateOfBirth(e) {
        this.setState({
            dob: e.target.value
        })
    }
    onAvatarChanged(e) {
        this.setState({
            avatar: e.target.files[0]
        })
    }
    onUpdateUser() {
        var apiService = new APIAccess();
        var newValues = {
            password: 'dummypassword',
            role: this.state.role,
            playRegion: this.state.region,
            country: this.state.country,
            language: this.state.language,
            dateOfBirth: this.state.dob
        }
        var modifiedUser = this.props.user;
        for (var key in newValues) {
            if (newValues[key] != null) {
                modifiedUser[key] = newValues[key]
            }
        }
        if(modifiedUser.dateOfBirth == "") {
            modifiedUser.dateOfBirth = "0001-01-01"
        }
        apiService.Put('users', this.props.user.id, JSON.stringify(modifiedUser), localStorage.getItem('token')).then(function (result) {
            if (result.ok) {
                alert('Information updated!')
                this.props.hideEditable();
            }
        }.bind(this))
    }
    onUploadAvatar() {
        var apiService = new APIAccess();
        var file = this.state.avatar;
        var avatarFile = new FormData();
        avatarFile.append('Avatar', file);
        avatarFile.append('UserName', this.props.user.username);
        apiService.UploadUserAvatar(avatarFile, localStorage.getItem('token')).then(function (result) {
            if(result.ok)
                alert('Avatar uploaded')
        }.bind(this))
    }
    render() {
        var date = this.props.user.dateOfBirth;
        if (this.props.user.dateOfBirth == '0001-01-01T00:00:00') {
            date = '';
        }
        return <div className="container profile profile-view" id="profile">
                <div className="row profile-row">
                    <div className="col-md-4 relative">
                        <img src={ this.state.useravatar } height="200" width="200"></img>
                        <div className="form-group">
                            <div className="col-md-10">
                                <p>Upload your avatar</p>
                                <input type="file" name="files" onChange={this.onAvatarChanged } />
                            </div>
                        </div>
                        <div className="form-group">
                            <div className="col-md-10">
                                <button className="btn btn-primary" onClick={ this.onUploadAvatar }>Upload</button>
                            </div>
                        </div>
                    </div>
                    <div className="col-md-8">
                        <h1>{ this.props.user.username } Profile</h1>
                        <hr />
                        <div className="row">
                            <div className="content-right">
                                <label className="control-label">Edit Region: </label>
                                <select className="form-control" onChange={ this.onChangeRegion }>
                                    <option value="">Select</option>
                                    <option value="Brazil">Brazil</option>
                                    <option value="Europe Nordic & East">Europe Nordic & East</option>
                                    <option value="Europe West">Europe West</option>
                                    <option value="Latin America North">Latin America North</option>
                                    <option value="Latin America South">Latin America South</option>
                                    <option value="Oceania">Oceania</option>
                                    <option value="Russia">Russia</option>
                                    <option value="Turkey">Turkey</option>
                                    <option value="Japan">Japan</option>
                                    <option value="South East Asia">South East Asia</option>
                                    <option value="Republic of Korea">Republic of Korea</option>
                                </select>
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <label className="control-label">Edit Role: </label>
                            <select className="form-control" onChange={ this.onChangeRole }>
                                <option value="">Select</option>
                                <option value="ADC">ADC</option>
                                <option value="Support">Support</option>
                                <option value="Jungle">Jungle</option>
                                <option value="Mid">Mid</option>
                                <option value="Top">Top</option>
                            </select>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="content-right">
                                <label className="control-label">Edit Country: </label>
                                <select className="form-control" onChange={ this.onChangeCountry }>
                                            <option value="">Select</option>
	                                        <option value="Afghanistan">Afghanistan</option>
	                                        <option value="Åland Islands">Åland Islands</option>
	                                        <option value="Albania">Albania</option>
	                                        <option value="Algeria">Algeria</option>
	                                        <option value="American Samoa">American Samoa</option>
	                                        <option value="Andorra">Andorra</option>
	                                        <option value="Angola">Angola</option>
	                                        <option value="Anguilla">Anguilla</option>
	                                        <option value="Antarctica">Antarctica</option>
	                                        <option value="Antigua and Barbuda">Antigua and Barbuda</option>
	                                        <option value="Argentina">Argentina</option>
	                                        <option value="Armenia">Armenia</option>
	                                        <option value="Aruba">Aruba</option>
	                                        <option value="Australia">Australia</option>
	                                        <option value="Austria">Austria</option>
	                                        <option value="Azerbaijan">Azerbaijan</option>
	                                        <option value="Bahamas">Bahamas</option>
	                                        <option value="Bahrain">Bahrain</option>
	                                        <option value="Bangladesh">Bangladesh</option>
	                                        <option value="Barbados">Barbados</option>
	                                        <option value="Belarus">Belarus</option>
	                                        <option value="Belgium">Belgium</option>
	                                        <option value="Belize">Belize</option>
	                                        <option value="Benin">Benin</option>
	                                        <option value="Bermuda">Bermuda</option>
	                                        <option value="Bhutan">Bhutan</option>
	                                        <option value="Bolivia">Bolivia</option>
	                                        <option value="Bosnia and Herzegovina">Bosnia and Herzegovina</option>
	                                        <option value="Botswana">Botswana</option>
	                                        <option value="Bouvet Island">Bouvet Island</option>
	                                        <option value="Brazil">Brazil</option>
	                                        <option value="Brunei Darussalam">Brunei Darussalam</option>
	                                        <option value="Bulgaria">Bulgaria</option>
	                                        <option value="Burkina Faso">Burkina Faso</option>
	                                        <option value="Burundi">Burundi</option>
	                                        <option value="Cambodia">Cambodia</option>
	                                        <option value="Cameroon">Cameroon</option>
	                                        <option value="Canada">Canada</option>
	                                        <option value="Cape Verde">Cape Verde</option>
	                                        <option value="Cayman Islands">Cayman Islands</option>
	                                        <option value="Central African Republic">Central African Republic</option>
	                                        <option value="Chad">Chad</option>
	                                        <option value="Chile">Chile</option>
	                                        <option value="China">China</option>
	                                        <option value="Colombia">Colombia</option>
	                                        <option value="Comoros">Comoros</option>
	                                        <option value="Congo">Congo</option>
	                                        <option value="Cook Islands">Cook Islands</option>
	                                        <option value="Costa Rica">Costa Rica</option>
	                                        <option value="Côte d'Ivoire">Côte d'Ivoire</option>
	                                        <option value="Croatia">Croatia</option>
	                                        <option value="Cuba">Cuba</option>
	                                        <option value="Curaçao">Curaçao</option>
	                                        <option value="Cyprus">Cyprus</option>
	                                        <option value="Czech Republic">Czech Republic</option>
	                                        <option value="Denmark">Denmark</option>
	                                        <option value="Djibouti">Djibouti</option>
	                                        <option value="Dominica">Dominica</option>
	                                        <option value="Dominican Republic">Dominican Republic</option>
	                                        <option value="Ecuador">Ecuador</option>
	                                        <option value="Egypt">Egypt</option>
	                                        <option value="El Salvador">El Salvador</option>
	                                        <option value="Equatorial Guinea">Equatorial Guinea</option>
	                                        <option value="Eritrea">Eritrea</option>
	                                        <option value="Estonia">Estonia</option>
	                                        <option value="Ethiopia">Ethiopia</option>
	                                        <option value="Falkland Islands (Malvinas)">Falkland Islands (Malvinas)</option>
	                                        <option value="Faroe Islands">Faroe Islands</option>
	                                        <option value="Fiji">Fiji</option>
	                                        <option value="Finland">Finland</option>
	                                        <option value="France">France</option>
	                                        <option value="Gabon">Gabon</option>
	                                        <option value="Gambia">Gambia</option>
	                                        <option value="Georgia">Georgia</option>
	                                        <option value="Germany">Germany</option>
	                                        <option value="Ghana">Ghana</option>
	                                        <option value="Gibraltar">Gibraltar</option>
	                                        <option value="Greece">Greece</option>
	                                        <option value="Greenland">Greenland</option>
	                                        <option value="Grenada">Grenada</option>
	                                        <option value="Guadeloupe">Guadeloupe</option>
	                                        <option value="Guam">Guam</option>
	                                        <option value="Guatemala">Guatemala</option>
	                                        <option value="Guernsey">Guernsey</option>
	                                        <option value="Guinea">Guinea</option>
	                                        <option value="Guinea-Bissau">Guinea-Bissau</option>
	                                        <option value="Guyana">Guyana</option>
	                                        <option value="Haiti">Haiti</option>
	                                        <option value="Honduras">Honduras</option>
	                                        <option value="Hong Kong">Hong Kong</option>
	                                        <option value="Hungary">Hungary</option>
	                                        <option value="Iceland">Iceland</option>
	                                        <option value="India">India</option>
	                                        <option value="Indonesia">Indonesia</option>
	                                        <option value="Iran">Iran</option>
	                                        <option value="Iraq">Iraq</option>
	                                        <option value="Ireland">Ireland</option>
	                                        <option value="Isle of Man">Isle of Man</option>
	                                        <option value="Israel">Israel</option>
	                                        <option value="Italy">Italy</option>
	                                        <option value="Jamaica">Jamaica</option>
	                                        <option value="Japan">Japan</option>
	                                        <option value="Jersey">Jersey</option>
	                                        <option value="Jordan">Jordan</option>
	                                        <option value="Kazakhstan">Kazakhstan</option>
	                                        <option value="Kenya">Kenya</option>
	                                        <option value="Kiribati">Kiribati</option>
	                                        <option value="South Korea">South Korea</option>
	                                        <option value="Kuwait">Kuwait</option>
	                                        <option value="Kyrgyzstan">Kyrgyzstan</option>
	                                        <option value="Lao">Lao</option>
	                                        <option value="Latvia">Latvia</option>
	                                        <option value="Lebanon">Lebanon</option>
	                                        <option value="Lesotho">Lesotho</option>
	                                        <option value="Liberia">Liberia</option>
	                                        <option value="Libya">Libya</option>
	                                        <option value="Liechtenstein">Liechtenstein</option>
	                                        <option value="Lithuania">Lithuania</option>
	                                        <option value="Luxembourg">Luxembourg</option>
	                                        <option value="Macao">Macao</option>
	                                        <option value="Macedonia">Macedonia</option>
	                                        <option value="Madagascar">Madagascar</option>
	                                        <option value="Malawi">Malawi</option>
	                                        <option value="Malaysia">Malaysia</option>
	                                        <option value="Maldives">Maldives</option>
	                                        <option value="Mali">Mali</option>
	                                        <option value="Malta">Malta</option>
	                                        <option value="Marshall Islands">Marshall Islands</option>
	                                        <option value="Martinique">Martinique</option>
	                                        <option value="Mauritania">Mauritania</option>
	                                        <option value="Mauritius">Mauritius</option>
	                                        <option value="Mayotte">Mayotte</option>
	                                        <option value="Mexico">Mexico</option>
	                                        <option value="Micronesia">Micronesia</option>
	                                        <option value="Moldova">Moldova</option>
	                                        <option value="Monaco">Monaco</option>
	                                        <option value="Mongolia">Mongolia</option>
	                                        <option value="Montenegro">Montenegro</option>
	                                        <option value="Montserrat">Montserrat</option>
	                                        <option value="Morocco">Morocco</option>
	                                        <option value="Mozambique">Mozambique</option>
	                                        <option value="Myanmar">Myanmar</option>
	                                        <option value="Namibia">Namibia</option>
	                                        <option value="Nauru">Nauru</option>
	                                        <option value="Nepal">Nepal</option>
	                                        <option value="Netherlands">Netherlands</option>
	                                        <option value="New Caledonia">New Caledonia</option>
	                                        <option value="New Zealand">New Zealand</option>
	                                        <option value="Nicaragua">Nicaragua</option>
	                                        <option value="Niger">Niger</option>
	                                        <option value="Nigeria">Nigeria</option>
	                                        <option value="Niue">Niue</option>
	                                        <option value="Norfolk Island">Norfolk Island</option>
	                                        <option value="Northern Mariana Islands">Northern Mariana Islands</option>
	                                        <option value="Norway">Norway</option>
	                                        <option value="Oman">Oman</option>
	                                        <option value="Pakistan">Pakistan</option>
	                                        <option value="Palau">Palau</option>
	                                        <option value="Palestinian">Palestinian</option>
	                                        <option value="Panama">Panama</option>
	                                        <option value="Papua New Guinea">Papua New Guinea</option>
	                                        <option value="Paraguay">Paraguay</option>
	                                        <option value="Peru">Peru</option>
	                                        <option value="Philippines">Philippines</option>
	                                        <option value="Pitcairn">Pitcairn</option>
	                                        <option value="Poland">Poland</option>
	                                        <option value="Portugal">Portugal</option>
	                                        <option value="Puerto Rico">Puerto Rico</option>
	                                        <option value="Qatar">Qatar</option>
	                                        <option value="Réunion">Réunion</option>
	                                        <option value="Romania">Romania</option>
	                                        <option value="Russia">Russia</option>
	                                        <option value="Rwanda">Rwanda</option>
	                                        <option value="Samoa">Samoa</option>
	                                        <option value="San Marino">San Marino</option>
	                                        <option value="Sao Tome and Principe">Sao Tome and Principe</option>
	                                        <option value="Saudi Arabia">Saudi Arabia</option>
	                                        <option value="Senegal">Senegal</option>
	                                        <option value="Serbia">Serbia</option>
	                                        <option value="Seychelles">Seychelles</option>
	                                        <option value="Sierra Leone">Sierra Leone</option>
	                                        <option value="Singapore">Singapore</option>
	                                        <option value="Sint Maarten (Dutch part)">Sint Maarten (Dutch part)</option>
	                                        <option value="Slovakia">Slovakia</option>
	                                        <option value="Slovenia">Slovenia</option>
	                                        <option value="Solomon Islands">Solomon Islands</option>
	                                        <option value="Somalia">Somalia</option>
	                                        <option value="South Africa">South Africa</option>
	                                        <option value="South Sudan">South Sudan</option>
	                                        <option value="Spain">Spain</option>
	                                        <option value="Sri Lanka">Sri Lanka</option>
	                                        <option value="Sudan">Sudan</option>
	                                        <option value="Suriname">Suriname</option>
	                                        <option value="Swaziland">Swaziland</option>
	                                        <option value="Sweden">Sweden</option>
	                                        <option value="Switzerland">Switzerland</option>
	                                        <option value="Syrian Arab Republic">Syrian Arab Republic</option>
	                                        <option value="Taiwan">Taiwan</option>
	                                        <option value="Tajikistan">Tajikistan</option>
	                                        <option value="Tanzania">Tanzania</option>
	                                        <option value="Thailand">Thailand</option>
	                                        <option value="Timor-Leste">Timor-Leste</option>
	                                        <option value="Togo">Togo</option>
	                                        <option value="Tokelau">Tokelau</option>
	                                        <option value="Tonga">Tonga</option>
	                                        <option value="Trinidad and Tobago">Trinidad and Tobago</option>
	                                        <option value="Tunisia">Tunisia</option>
	                                        <option value="Turkey">Turkey</option>
	                                        <option value="Turkmenistan">Turkmenistan</option>
	                                        <option value="Tuvalu">Tuvalu</option>
	                                        <option value="Uganda">Uganda</option>
	                                        <option value="Ukraine">Ukraine</option>
	                                        <option value="United Arab Emirates">United Arab Emirates</option>
	                                        <option value="United Kingdom">United Kingdom</option>
	                                        <option value="United States">United States</option>
	                                        <option value="Uruguay">Uruguay</option>
	                                        <option value="Uzbekistan">Uzbekistan</option>
	                                        <option value="Vanuatu">Vanuatu</option>
	                                        <option value="Venezuela">Venezuela</option>
	                                        <option value="Vietnam">Vietnam</option>
	                                        <option value="Western Sahara">Western Sahara</option>
	                                        <option value="Yemen">Yemen</option>
	                                        <option value="Zambia">Zambia</option>
	                                        <option value="Zimbabwe">Zimbabwe</option>
                                </select>
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="content-right">
                                <label className="control-label">Edit Language: </label>
                                <select className="form-control" onChange={ this.onChangeLanguage }>
                                            <option value="">Select</option>
                                            <option value="Afrikaans">Afrikaans</option>
                                            <option value="Albanian">Albanian</option>
                                            <option value="Amharic">Amharic</option>
                                            <option value="Arabic">Arabic</option>
                                            <option value="Armenian">Armenian</option>
                                            <option value="Azerbaijani">Azerbaijani</option>
                                            <option value="Basque">Basque</option>
                                            <option value="Belarusian">Belarusian</option>
                                            <option value="Bengali">Bengali</option>
                                            <option value="Bosnian">Bosnian</option>
                                            <option value="Bulgarian">Bulgarian</option>
                                            <option value="Catalan">Catalan</option>
                                            <option value="Cebuano">Cebuano</option>
                                            <option value="Chichewa">Chichewa</option>
                                            <option value="Chinese">Chinese</option>
                                            <option value="Corsican">Corsican</option>
                                            <option value="Croatian">Croatian</option>
                                            <option value="Czech">Czech</option>
                                            <option value="Danish">Danish</option>
                                            <option value="Dutch">Dutch</option>
                                            <option value="English">English</option>
                                            <option value="Esperanto">Esperanto</option>
                                            <option value="Estonian">Estonian</option>
                                            <option value="Filipino">Filipino</option>
                                            <option value="Finnish">Finnish</option>
                                            <option value="French">French</option>
                                            <option value="Frisian">Frisian</option>
                                            <option value="Galician">Galician</option>
                                            <option value="German">German</option>
                                            <option value="Greek">Greek</option>
                                            <option value="Gujarati">Gujarati</option>
                                            <option value="Haitian Creole">Haitian Creole</option>
                                            <option value="Hausa">Hausa</option>
                                            <option value="Hawaiian">Hawaiian</option>
                                            <option value="Hebrew">Hebrew</option>
                                            <option value="Hindi">Hindi</option>
                                            <option value="Hmong">Hmong</option>
                                            <option value="Hungarian">Hungarian</option>
                                            <option value="Icelandic">Icelandic</option>
                                            <option value="Igbo">Igbo</option>
                                            <option value="Indonesian">Indonesian</option>
                                            <option value="Irish">Irish</option>
                                            <option value="Italian">Italian</option>
                                            <option value="Japanese">Japanese</option>
                                            <option value="Javanese">Javanese</option>
                                            <option value="Kannada">Kannada</option>
                                            <option value="Kazakh">Kazakh</option>
                                            <option value="Khmer">Khmer</option>
                                            <option value="Korean">Korean</option>
                                            <option value="Kurdish (Kurmanji)">Kurdish (Kurmanji)</option>
                                            <option value="Kyrgyz">Kyrgyz</option>
                                            <option value="Lao">Lao</option>
                                            <option value="Latin">Latin</option>
                                            <option value="Latvian">Latvian</option>
                                            <option value="Lithuanian">Lithuanian</option>
                                            <option value="Luxembourgish">Luxembourgish</option>
                                            <option value="Macedonian">Macedonian</option>
                                            <option value="Malagasy">Malagasy</option>
                                            <option value="Malay">Malay</option>
                                            <option value="Malayalam">Malayalam</option>
                                            <option value="Maltese">Maltese</option>
                                            <option value="Maori">Maori</option>
                                            <option value="Marathi">Marathi</option>
                                            <option value="Mongolian">Mongolian</option>
                                            <option value="Myanmar (Burmese)">Myanmar (Burmese)</option>
                                            <option value="Nepali">Nepali</option>
                                            <option value="Norwegian">Norwegian</option>
                                            <option value="Pashto">Pashto</option>
                                            <option value="Persian">Persian</option>
                                            <option value="Polish">Polish</option>
                                            <option value="Portuguese">Portuguese</option>
                                            <option value="Punjabi">Punjabi</option>
                                            <option value="Romanian">Romanian</option>
                                            <option value="Russian">Russian</option>
                                            <option value="Samoan">Samoan</option>
                                            <option value="Scots Gaelic">Scots Gaelic</option>
                                            <option value="Serbian">Serbian</option>
                                            <option value="Sesotho">Sesotho</option>
                                            <option value="Shona">Shona</option>
                                            <option value="Sindhi">Sindhi</option>
                                            <option value="Sinhala">Sinhala</option>
                                            <option value="Slovak">Slovak</option>
                                            <option value="Slovenian">Slovenian</option>
                                            <option value="Somali">Somali</option>
                                            <option value="Spanish">Spanish</option>
                                            <option value="Sundanese">Sundanese</option>
                                            <option value="Swahili">Swahili</option>
                                            <option value="Swedish">Swedish</option>
                                            <option value="Tajik">Tajik</option>
                                            <option value="Tamil">Tamil</option>
                                            <option value="Telugu">Telugu</option>
                                            <option value="Thai">Thai</option>
                                            <option value="Turkish">Turkish</option>
                                            <option value="Ukrainian">Ukrainian</option>
                                            <option value="Urdu">Urdu</option>
                                            <option value="Uzbek">Uzbek</option>
                                            <option value="Vietnamese">Vietnamese</option>
                                            <option value="Welsh">Welsh</option>
                                            <option value="Xhosa">Xhosa</option>
                                            <option value="Yiddish">Yiddish</option>
                                            <option value="Yoruba">Yoruba</option>
                                            <option value="Zulu">Zulu</option>
                                </select>
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="content-right">
                                <label className="control-label">Edit Date of birth: </label>
                                <input type="date" name="bday" onChange={ this.onChangeDateOfBirth } />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-md-12 content-right">
                                <button className="btn btn-primary form-btn" type="submit" onClick={this.onUpdateUser}>Save Changes</button>
                                <button className="btn btn-primary form-btn" type="submit" onClick={this.props.hideEditable}>Back To Profile</button>
                            </div>
                        </div>
                    </div>
                </div>
        </div>
    }
}
module.exports = ProfilePage;