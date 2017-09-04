import React from 'react';
import ReactDOM from 'react-dom'
import { BrowserRouter as Router, Route, hashHistory, Link } from 'react-router-dom'
var NavbarComponent = require('./navbarComponent.jsx')
var HomePage = require('./homePageComponent.jsx')
var UsersPage = require('./usersPageComponent.jsx')
var TeamsPage = require('./teamsPageComponent.jsx')
var TournamentsPage = require('./tournamentsPageComponent.jsx')
var ProfilePage = require('./profileComponent.jsx')
var TeamProfilePage = require('./teamProfilePageComponent.jsx')
var AboutPage = require('./aboutPageComponent.jsx')
var FooterComponent = require('./footerComponent.jsx')

class MainPage extends React.Component {
    render() {
        return (
            <Router>
                <div>
                    <NavbarComponent />
                    <Route exact path="/mainPage" component={HomePage} />
                    <Route path="/users" component={UsersPage} />
                    <Route path="/teams" component={TeamsPage} />
                    <Route path="/tournaments" component={TournamentsPage} />
                    <Route path="/profile" component={ProfilePage} />
                    <Route path="/teamprofile" component={TeamProfilePage} />
                    <Route path="/about" component={AboutPage} />
                    <FooterComponent />
                </div>
            </Router>
        );
    }
}

ReactDOM.render(
    <MainPage />,
    document.getElementById('mainPage')
);