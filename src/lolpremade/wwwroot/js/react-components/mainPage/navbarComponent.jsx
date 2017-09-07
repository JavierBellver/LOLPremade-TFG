import { Link } from 'react-router-dom'

class NavbarComponent extends React.Component {
    logout() {
        localStorage.removeItem('username');
        localStorage.removeItem('token');
        window.location.href = '/landingPage';
    }
    render() {
        return (
            <nav className="navbar navbar-default navigation-clean-button">
                <div className="container">
                    <div className="navbar-header">
                        <div className="navbar-brand navbar-link"><Link to="/mainPage">Lolpremade</Link></div>
                        <button className="navbar-toggle collapsed" data-toggle="collapse" data-target="#navcol-1"><span className="sr-only">Toggle navigation</span><span className="icon-bar"></span><span className="icon-bar"></span><span className="icon-bar"></span></button>
                    </div>
                    <div className="collapse navbar-collapse" id="navcol-1">
                        <ul className="nav navbar-nav">
                            <li><Link to="/users">Players</Link></li>
                            <li role="presentation"><Link to="/teams">Teams</Link></li>
                            <li role="presentation"><Link to="/tournaments">Tournaments</Link></li>
                        </ul>
                        <p className="navbar-text navbar-right actions">
                            <button className="btn btn-default action-button"><Link to="/profile">My Profile</Link></button>
                            <button className="btn btn-default action-button" onClick={ this.logout }>Logout</button>
                        </p>
                    </div>
                </div>
            </nav>
        );
    }
}
module.exports = NavbarComponent;