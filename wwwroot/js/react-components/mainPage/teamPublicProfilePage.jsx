class TeamPublicProfileComponent extends React.Component {
    constructor() {
        super();
        this.state = { team: {}, admin: {}, teamavatar: '' }

        this.getTeamData = this.getTeamData.bind(this);
        this.getTeamAdmin = this.getTeamAdmin.bind(this);
        this.checkImageExists = this.checkImageExists.bind(this);
        this.componentDidMount = this.componentDidMount.bind(this);
    }
    getTeamData() {
        var apiService = new APIAccess();
        return apiService.GetById('teams', this.props.teamid).then(function (result) {
            return result;
        })
    }
    getTeamAdmin() {
        var apiService = new APIAccess();
        if (this.state.team.teamAdmin != 0) {
            return apiService.GetById('users', this.state.team.teamAdmin).then(function (result) {
                return result;
            })
        }
    }
    checkImageExists(imageSrc) {
        var img = new Image();
        img.onload = function () { this.setState({ teamavatar: imageSrc }) }.bind(this);
        img.onerror = function () { this.setState({ teamavatar: 'images/add_friend_article_banner.jpg' }) }.bind(this);
        img.src = imageSrc;
    }
    componentDidMount() {
        this.getTeamData().then(function (result) {
            this.setState({
                team: result
            })
            this.getTeamAdmin(function (result) {
                this.setState({
                    admin: result
                })
            }.bind(this))
            var route = 'images/teams/' + this.state.team.name + 'avatar.jpeg';
            this.checkImageExists(route);
        }.bind(this))
    }
    render() {
        return <div className="center-block text-center">
                <img height="200" width="400" src={this.state.teamavatar }></img>
                <h1>{ this.state.team.name}</h1>
                <p>Description: { this.state.team.description}</p>
                <p>Rank: { this.state.team.rank}</p>
                <p>Region: { this.state.team.playRegion}</p>
                <p>Country: { this.state.team.country}</p>
                <p>Language: { this.state.team.language}</p>
                <p>Admin: { this.state.admin.username }</p>
            </div>
    }
}
module.exports = TeamPublicProfileComponent;