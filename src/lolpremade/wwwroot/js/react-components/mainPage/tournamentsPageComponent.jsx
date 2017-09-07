class TournamentCard extends React.Component {
    constructor() {
        super();
        this.state = { authorized: false, teamid: 0 }

        this.checkParticipantValidity = this.checkParticipantValidity.bind(this);
        this.onSubmitTournament = this.onSubmitTournament.bind(this);
        this.onJoinTournamentClicked = this.onJoinTournamentClicked.bind(this);
    }
    checkParticipantValidity() {
        var apiService = new APIAccess();
        return apiService.GetById('users', localStorage.getItem('username')).then(function (user) {
            if (user != null && user.pertainingTeam != 0) {
                if (this.props.tournament.numberOfCurrentParticipants < this.props.tournament.numberOfParticipantTeams) {
                    return apiService.GetById('teams', user.pertainingTeam).then(function (team) {
                        if (team.teamAdmin == user.id) {
                            this.setState({
                                authorized: true,
                                teamid: team.id
                            })
                        }
                        else {
                            alert('You need to be the team admin to join the tournament')
                        }
                    }.bind(this))
                }
                else {
                    alert('Sorry, this tournament is full')
                }
            }
            else {
                alert('You cant join a tournament, you have no team')
            }
        }.bind(this))
    }
    onSubmitTournament() {
        this.checkParticipantValidity().then(function () {
            if (this.state.authorized) {
                var apiService = new APIAccess();
                var participant = {
                    TournamentId: this.props.tournament.id,
                    TeamId: this.state.teamid
                }
                apiService.PostAuthorized('tournamentparticipants', JSON.stringify(participant), localStorage.getItem('token')).then(function (result) {
                    if (result.ok) {
                        alert('Your team joined successfully to' + this.props.tournament.name);
                    }
                    else {
                        alert('Error joining to the tournament');
                    }
                }.bind(this))
            }
        }.bind(this))
    }
    onJoinTournamentClicked() {
        if (confirm("Do you really want to join this tournament") == true) {
            this.onSubmitTournament();
        }
    }
    render() {
        return <div className="col-md-6" key={ this.props.tournament.id}>
            <div className="card">
                <img height="200" width="100" className="card-image" src={ this.props.tournament.tournamentAvatar } />
                <div className="card-container">
                    <h4><b>{ this.props.tournament.name }</b></h4>
                    <p>Rank: { this.props.tournament.rank } </p>
                    <p>Country: { this.props.tournament.country }</p>
                    <p>Language: { this.props.tournament.language }</p>
                    <p>Organizer: { this.props.tournament.organizerName } </p>
                    <p>Maximum participants: { this.props.tournament.numberOfParticipantTeams}</p>
                    <p>Available Spaces: { this.props.tournament.numberOfParticipantTeams - this.props.tournament.numberOfCurrentParticipants}</p>
                </div>
                <div className="panel-footer">
                    <p><span className="glyphicon glyphicon-plus"></span><button onClick={ this.onJoinTournamentClicked }>Join Tournament</button></p>
                    <p><span className="glyphicon glyphicon-user"></span>See Tournament's Profile</p>
                </div>
            </div>
        </div>
    }
}

class TournamentsPage extends React.Component {
    constructor() {
        super();
        this.state = { status: 'REQUEST', tournaments: [], filteredTournaments: [], searchedTournament: '', chosenRank: '',chosenLanguage: '',chosenCountry: '', chosenOrganizer: '', chosenMaximumParticipants: '', placesAvailable: false }

        this.initialLoad = this.initialLoad.bind(this);
        this.onChangeTournamentName = this.onChangeTournamentName.bind(this);
        this.onRankChanged = this.onRankChanged.bind(this);
        this.onLanguageChange = this.onLanguageChange.bind(this);
        this.onCountryChanged = this.onCountryChanged.bind(this);
        this.onOrganizerChanged = this.onOrganizerChanged.bind(this);
        this.onMaxParticipantsChanged = this.onMaxParticipantsChanged.bind(this);
        this.onToggleWithAvailablePlaces = this.onToggleWithAvailablePlaces.bind(this);
        this.filterTournaments = this.filterTournaments.bind(this);
        this.submitFilterForm = this.submitFilterForm.bind(this);
        this.renderLoadingSpinner = this.renderLoadingSpinner.bind(this);
        this.renderList = this.renderList.bind(this);
        this.initialLoad();
    }
    initialLoad() {
        var apiService = new APIAccess();
        apiService.Get('tournaments').then(function (result) {
            if (result.length > 0) {
                this.setState({
                    tournaments: result,
                    filteredTournaments: result,
                    status: 'RESOLVED'
                })
            }
        }.bind(this))
    }
    onChangeTournamentName(e) {
        this.setState({
            searchedTournament: e.target.value
        })
    }
    onRankChanged(e) {
        this.setState({
            chosenRank: e.target.value
        })
    }
    onLanguageChange(e) {
        this.setState({
            chosenLanguage: e.target.value
        })
    }
    onCountryChanged(e) {
        this.setState({
            chosenCountry: e.target.value
        })
    }
    onMaxParticipantsChanged(e) {
        this.setState({
            chosenMaximumParticipants: e.target.value
        })
    }
    onOrganizerChanged(e) {
        this.setState({
            chosenOrganizer: e.target.value
        })
    }
    onToggleWithAvailablePlaces(e) {
        this.setState({
            placesAvailable: !this.state.placesAvailable
        })
    }
    filterTournaments() {
        var filter = {
            rank: this.state.chosenRank,
            language: this.state.chosenLanguage,
            country: this.state.chosenCountry,
            organizerName: this.state.chosenOrganizer,
            numberOfParticipantTeams: this.state.chosenMaximumParticipants
        }
        var filtered = this.state.tournaments.filter(function (tournament) {
            for (var key in filter) {
                if ((tournament[key] === undefined || tournament[key] != filter[key]) && filter[key] != "")
                    return false;
            }
            if (this.state.placesAvailable) {
                if ((tournament.numberOfParticipantTeams - tournament.numberOfCurrentParticipants) > 0) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return true;
            }
        }.bind(this))
        this.setState({
            filteredTournaments: filtered
        })
    }
    submitFilterForm() {
        var apiService = new APIAccess();
        if (this.state.searchedTournament != undefined && this.state.searchedTournament != '') {
            apiService.GetById('tournaments', this.state.searchedTournament).then(function (result) {
                if (result.id == 0) {
                    this.setState({
                        filteredTournaments: [],
                        status: 'RESOLVED'
                    })
                }
                else {
                    this.setState({
                        filteredTournaments: [result],
                        status: 'RESOLVED'
                    })
                }
            }.bind(this))
        }
        else {
            this.filterTournaments();
        }
    }
    renderLoadingSpinner() {
        return <span className="text-center">Loading. . .</span>
    }
    renderList(items) {
        return <div className="col-md-9">
                    {items}
               </div>
    }
    render() {
        var items = [];
        for(var i=0;i<this.state.filteredTournaments.length;i++) {
            items.push(<TournamentCard key={ i } tournament={ this.state.filteredTournaments[i] } />)
        }
        return  (<div>
                    <h1 className="text-center">Search for Tournaments!</h1>
                    <div className="container">
                        <div className="row">
                            <div className="col-md-3">
                                <form>
                                    <div className="form-group">
                                        <label className="control-label">Tournament Name </label>
                                        <input type="text" className="form-control" onChange={ this.onChangeTournamentName } />
                                    </div>
                                    <div className="form-group">
                                        <label className="control-label">Rank</label>
                                        <select className="form-control" onChange={ this.onRankChanged }>
                                            <option value="">Select</option>
                                            <option value="UNRANKED">Unranked</option>
                                            <optgroup label="Bronze">
                                                <option value="BRONZE V">Bronze V</option>
                                                <option value="BRONZE IV">Bronze IV</option>
                                                <option value="BRONZE III">Bronze III</option>
                                                <option value="BRONZE II">Bronze II</option>
                                                <option value="BRONZE I">Bronze I</option>
                                            </optgroup>
                                            <optgroup label="Silver">
                                                <option value="SILVER V">Silver V</option>
                                                <option value="SILVER IV">Silver IV</option>
                                                <option value="SILVER III">Silver III</option>
                                                <option value="SILVER II">Silver II</option>
                                                <option value="SILVER I">Silver I</option>
                                            </optgroup>
                                            <optgroup label="Gold">
                                                <option value="GOLD V">Gold V</option>
                                                <option value="GOLD IV">Gold IV</option>
                                                <option value="GOLD III">Gold III</option>
                                                <option value="GOLD II">Gold II</option>
                                                <option value="GOLD I">Gold I</option>
                                            </optgroup>
                                            <optgroup label="Platinum">
                                                <option value="PLATINUM V">Platinum V</option>
                                                <option value="PLATINUM IV">Platinum IV</option>
                                                <option value="PLATINUM III">Platinum III</option>
                                                <option value="PLATINUM II">Platinum II</option>
                                                <option value="PLATINUM I">Platinum I</option>
                                            </optgroup>
                                            <optgroup label="Diamond">
                                                <option value="DIAMOND V">Diamond V</option>
                                                <option value="DIAMOND IV">Diamond IV</option>
                                                <option value="DIAMOND III">Diamond III</option>
                                                <option value="DIAMOND II">Diamond II</option>
                                                <option value="DIAMOND I">Diamond I</option>
                                            </optgroup>
                                            <option value="MASTER">Master</option>
                                            <option value="CHALLENGER">Challenger</option>
                                        </select>
                                    </div>
                                    <div className="form-group">
                                        <label className="control-label">Language </label>
                                        <select className="form-control" onChange={ this.onLanguageChange }>
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
                                <div className="form-group">
                                    <label className="control-label">Country </label>
                                    <select className="form-control" onChange={ this.onCountryChanged }>
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
                                <div className="form-group">
                                    <label className="control-label">Maximum participants</label>
                                    <input type="text" className="form-control" onChange={ this.onMaxParticipantsChanged } />
                                </div>
                                <div className="form-group">
                                    <label className="control-label">Organizer's name</label>
                                    <input type="text" className="form-control" onChange={ this.onOrganizerChanged } />
                                </div>
                                <div className="form-group">
                                    <label className="control-label">
                                        <input type="checkbox"
                                        value="SearchAvailableSpaces"
                                        onChange={ this.onToggleWithAvailablePlaces } />Mark to search tournaments with empty places
                             </label>
                         </div>
                         <div className="form-group">
                             <input className="btn btn-primary btn-block" type="button" value="SEARCH!" onClick={ this.submitFilterForm } />
                         </div>
                                </form>
                            </div>
                            { this.state.status == "REQUEST" ? this.renderLoadingSpinner() : this.renderList(items) }
                        </div>
                    </div>
                </div>
            );
    }
}
module.exports = TournamentsPage;