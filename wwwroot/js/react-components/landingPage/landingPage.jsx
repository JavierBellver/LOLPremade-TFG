import React from 'react';
import ReactDOM from 'react-dom'
import { BrowserRouter as Router, Route, hashHistory, Link } from 'react-router-dom'
var LandingComponent = require('./landingComponent.jsx')
var LoginComponent = require('./loginComponent.jsx')
var RegisterComponent = require('./registerComponent.jsx')


class LandingPage extends React.Component {
    render() {
        return (
            <Router>
                <div>
                    <Route exact path="/" component={LandingComponent} />
                    <Route exact path="/landingPage" component={LandingComponent} />
                    <Route path="/login" component={LoginComponent} />
                    <Route path="/register" component={RegisterComponent} />
                </div>
            </Router>
        );
    }
}


ReactDOM.render(
  <LandingPage />,
  document.getElementById('landing-container')
);
