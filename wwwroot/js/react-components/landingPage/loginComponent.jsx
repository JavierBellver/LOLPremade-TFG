import { Link } from 'react-router-dom';

class LoginComponent extends React.Component {
    constructor() {
        super();
        this.state = { username: '', password: '', errors: [] };

        this.onLoginClicked = this.onLoginClicked.bind(this)
        this.onUpdateUsername = this.onUpdateUsername.bind(this)
        this.onUpdatePassword = this.onUpdatePassword.bind(this)
    }
    onLoginClicked() {
        var apiService = new APIAccess();
        var username = this.state.username;
        var tokenPromise = apiService.GetToken(this.state.username, this.state.password).then(function (result) {
            if (result != undefined && result.access_token != null) {
                localStorage.setItem('token', result.access_token)
                localStorage.setItem('username', username)
                alert('Success on login operation');
                window.location.href = '/mainPage';
            }
            else {
                var newErrors = this.state.errors;
                newErrors.push('Error, login operation failed')
                this.setState({
                    errors: newErrors
                })
            }
        }.bind(this));
    }
    onUpdateUsername(e) {
        this.setState({
            username: e.target.value
        })
    }
    onUpdatePassword(e) {
        this.setState({
            password: e.target.value
        })
    }
    render() {
        var errors = [];
        for (var i = 0; i < this.state.errors.length; i++) {
            var elemento = <li key={i}>{this.state.errors[i]}</li>
            errors.push(elemento)
        }
        return (
                <div className="login-dark">
                    <ul id="errorList">{errors}</ul>
                    <form>
                        <h2 className="sr-only">Login Form</h2>
                        <div className="illustration"><i className="icon ion-ios-locked-outline"></i></div>
                        <div className="form-group">
                            <input className="form-control" type="text" name="username" placeholder="Username" onChange={ this.onUpdateUsername } />
                        </div>
                        <div className="form-group">
                            <input className="form-control" type="password" name="password" placeholder="Password" onChange={ this.onUpdatePassword } />
                        </div>
                        <div className="form-group">
                            <input className="btn btn-primary btn-block" type="button" value="LogIn" onClick={ this.onLoginClicked } />
                        </div><a href="#" className="forgot">Forgot your email or password?</a>
                    </form>
                </div>
        );
    }
};
module.exports = LoginComponent;