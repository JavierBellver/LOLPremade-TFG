class TeamPublicProfileComponent extends React.Component {
    constructor() {
        super();
        this.state = { team: {}, admin: {} }

        this.getTeamData = this.getTeamData.bind(this);
        this.getTeamAdmin = this.getTeamAdmin.bind(this);
        this.onGoBackClicked = this.onGoBackClicked.bind(this);
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
    onGoBackClicked() {
        this.props.goBack();
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
        }.bind(this))
    }
    render() {
        return <div className="center-block text-center">
                <img height="200" width="400" src={this.state.team.teamAvatar }></img>
                <h1>{ this.state.team.name}</h1>
                <p>Description: { this.state.team.description}</p>
                <p>Rank: { this.state.team.rank}</p>
                <p>Region: { this.state.team.playRegion}</p>
                <p>Country: { this.state.team.country}</p>
                <p>Language: { this.state.team.language}</p>
                <p>Admin: { this.state.admin.username }</p>
                <button className="btn btn-primary" onClick={ this.onGoBackClicked }>Go back to User List</button>
            </div>
    }
}
module.exports = TeamPublicProfileComponent;