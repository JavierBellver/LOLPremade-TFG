import ReactStars from 'react-stars'
import Modal from 'react-modal';

class OpinionComponent extends React.Component {
    render() {
        return <article>
                    <header>Comentario de: { this.props.opinion.receivingUserId }</header>
                    <label>{ this.props.opinion.shortText } <ReactStars count={5} size={12} color2={'#ffd700'} edit={ false } value={ this.props.opinion.punctuation }/></label>
                    <p>{ this.props.opinion.commentText }</p>
                    <footer>
                        <p>Posted on: { this.props.opinion.opinionDate }</p>
                    </footer>
                </article>
    }
}

class OpinionsComponent extends React.Component {
    constructor() {
        super();
        this.state = { opinions: [], addCommentClicked: false, shortComment: '', commentText: '', punctuation: 0 }

        this.getOpinions = this.getOpinions.bind(this);
        this.openModal = this.openModal.bind(this);
        this.closeModal = this.closeModal.bind(this);
        this.onUpdateShortComment = this.onUpdateShortComment.bind(this);
        this.onUpdateCommentText = this.onUpdateCommentText.bind(this);
        this.onUpdatePunctuation = this.onUpdatePunctuation.bind(this);
        this.onOpinionSubmit = this.onOpinionSubmit.bind(this);
    }
    getOpinions() {
        var apiService = new APIAccess();
        return apiService.GetById('useropinions', this.props.userid).then(function (result) {
            return result;
        })
    }
    openModal() {
        this.setState({
            addCommentClicked: true
        })
    }
    closeModal() {
        this.setState({
            addCommentClicked: false
        })
    }
    onUpdateShortComment(e) {
        this.setState({
            shortComment: e.target.value
        })
    }
    onUpdateCommentText(e) {
        this.setState({
            commentText: e.target.value
        })
    }
    onUpdatePunctuation(newPunctuation) {
        this.setState({
            punctuation: newPunctuation
        })
    }
    onOpinionSubmit() {
        var apiService = new APIAccess();
        apiService.GetById('users',localStorage.getItem('username')).then(function(result) {
            if(result != null) {
                var opinion = {
                    SenderUserId: result.id,
                    ReceivingUserId: this.props.userid,
                    shortText: this.state.shortComment,
                    commentText: this.state.commentText,
                    punctuation: this.state.punctuation
                }
                apiService.PostAuthorized('useropinions',JSON.stringify(opinion),localStorage.getItem('token')).then(function(result) {
                    if(result.ok) {
                        var updatedOpinions = this.state.opinions;
                        opinion.opinionDate = new Date().toLocaleString();
                        updatedOpinions.push(opinion);
                        this.setState({
                            opinions: updatedOpinions,
                            addCommentClicked: false
                        })
                    }
                    else {
                        alert('Error, opinion did not get published correctly');
                    }
                }.bind(this)).catch(function(err){
                    alert('Error, opinion did not get published correctly');
                })
            }
            else {
                alert('Error, you must be logged in to post a comment');
            }
        }.bind(this))
    }
    componentDidMount() {
        this.getOpinions().then(function (result) {
            this.setState({
                opinions: result
            })
        }.bind(this))
    }
    render() {
        var opinionComponents = []
        if (this.state.opinions.length > 0) {
            for (var i = 0; i < this.state.opinions.length; i++) {
                opinionComponents.push(<OpinionComponent key={i} opinion={ this.state.opinions[i] } />)
            }
        }
        return  <section>
                    <Modal isOpen={this.state.addCommentClicked}
                        onRequestClose={this.closeModal}
                        contentLabel="Example Modal">

                        <form>
                            <div className="form-group">
                                <label className="control-label">Short comment description: </label>
                                <input type="text" className="form-control" placeholder="Short comment" onChange={ this.onUpdateShortComment } />
                            </div>
                            <div className="form-group">
                                <label className="control-label">Punctuation: </label>
                                <ReactStars value={ this.state.punctuation } count={5} size={24} color2={'#ffd700'} onChange={ this.onUpdatePunctuation }/>
                            </div>
                            <div className="form-group">
                                <label className="control-label">Details: </label>
                                <textarea className="form-control" placeholder="Long form comment" onChange={ this.onUpdateCommentText } />
                            </div>
                            <button className="btn btn-primary" type="button" onClick={ this.onOpinionSubmit }>Upload Opinion</button>
                        </form>
                    </Modal>
                    <header><h2>Opinions about the user</h2></header>
                    <div className="container">
                        { opinionComponents }
                    </div>
                    <button className="" onClick={ this.openModal }>Add a new comment about this user</button>
                </section>
        }
    }

class UserPublicProfileComponent extends React.Component {
    constructor() {
        super();
        this.state = { user: {}, useravatar: '' }

        this.getUserData = this.getUserData.bind(this);
        this.checkImageExists = this.checkImageExists.bind(this);
        this.componentDidMount = this.componentDidMount.bind(this);
    }
    getUserData() {
        var apiService = new APIAccess();
        return apiService.GetById('users', this.props.userid).then(function (result) {
            return result;
        })
    }
    checkImageExists(imageSrc) {
        var img = new Image();
        img.onload = function () { this.setState({ teamavatar: imageSrc }) }.bind(this);
        img.onerror = function () { this.setState({ teamavatar: 'images/add_friend_article_banner.jpg' }) }.bind(this);
        img.src = imageSrc;
    }
    componentDidMount() {
        this.getUserData().then(function (result) {
            this.setState({
                user: result
            })
            var route = 'images/users/' + this.state.user.username + 'avatar.jpeg';
            this.checkImageExists(route);
        }.bind(this))
    }
    render() {
        return <div className="center-block text-center">
                    <img height="200" width="400" src={this.state.useravatar }></img>
                    <h1>{ this.state.user.username}</h1>
                    <p>Level: { this.state.user.level}</p>
                    <p>Rank: { this.state.user.rank}</p>
                    <p>Region: { this.state.user.playRegion}</p>
                    <p>Country: { this.state.user.country}</p>
                    <p>Language: { this.state.user.language}</p>
                    <OpinionsComponent userid={this.props.userid} />
               </div>
    }
}
module.exports = UserPublicProfileComponent;