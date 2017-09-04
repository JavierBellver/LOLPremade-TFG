class RegisterComponent extends React.Component {
    constructor() {
        super();
        this.state = { email: '', username: '', password: '', passwordRepeat: '', termschecked: false, errors: [] };

        this.onRegisterClick = this.onRegisterClick.bind(this)
        this.onUpdateEmail = this.onUpdateEmail.bind(this)
        this.onUpdateUsername = this.onUpdateUsername.bind(this)
        this.onUpdatePassword = this.onUpdatePassword.bind(this)
        this.onUpdatePasswordRepeat = this.onUpdatePasswordRepeat.bind(this)
        this.toggleCheckbox = this.toggleCheckbox.bind(this)
        this.onAlreadyHaveAnAccount = this.onAlreadyHaveAnAccount.bind(this)
    }
    onRegisterClick() {
        var apiService = new APIAccess();
        if (!this.state.termschecked) {
            var newErrors = this.state.errors;
            newErrors.push('Please, accept terms and conditions')
            this.setState({
                errors: newErrors
            })
        }
        else if (this.state.password == this.state.passwordRepeat) {
            var registerRequest = {
                email: this.state.email,
                username: this.state.username,
                password: this.state.password
            }
            apiService.Post('users', JSON.stringify(registerRequest))
                .then(result => {
                    if (result.status != 200) {
                        return result.json().then(result => {
                            var newErrors = this.state.errors;
                            if (result.message)
                                newErrors.push(result.message)
                            else
                                newErrors.push('An error ocurred during registration')
                            this.setState({
                                errors: newErrors
                            })
                        });
                    }
                    else {
                        alert("Exito al registrarse")
                        window.location.href = '/login'
                    }
                })
        }
        else {
            var newErrors = this.state.errors;
            newErrors.push('Please, insert matching passwords')
            this.setState({
                errors: newErrors
            })
        }
    }
    onUpdateEmail(e) {
        this.setState({
            email: e.target.value
        })
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
    onUpdatePasswordRepeat(e) {
        this.setState({
            passwordRepeat: e.target.value
        })
    }
    toggleCheckbox(e) {
        this.setState({
            termschecked: !this.state.termschecked
        })
    }
    onAlreadyHaveAnAccount() {
        window.location.href = '/login'
    }
    render() {
        var errors = [];
        for (var i = 0; i < this.state.errors.length; i++) {
            var elemento = <li key={i}>{this.state.errors[i]}</li>
            errors.push(elemento)
        }

        return (
                <div className="register-photo">
                    <ul id="errorList">{errors}</ul>
                    <div className="form-container">
                        <div className="image-holder"></div>
                        <form>
                            <h2 className="text-center"><strong>Create</strong> an account.</h2>
                            <div className="form-group">
                                <input className="form-control" type="email" name="email" placeholder="Email" onChange={ this.onUpdateEmail }/>
                            </div>
                            <div className="form-group">
                                <input className="form-control" type="text" name="username" placeholder="Username" onChange={ this.onUpdateUsername } />
                            </div>
                            <div className="form-group">
                                <input className="form-control" type="password" name="password" placeholder="Password" onChange={ this.onUpdatePassword } />
                            </div>
                            <div className="form-group">
                                <input className="form-control" type="password" name="password-repeat" placeholder="Password (repeat)" onChange={ this.onUpdatePasswordRepeat } />
                            </div>
                            <div className="form-group">
                                <label>
                                  <input type="checkbox"
                                         value="Terms and conditions"
                                         onChange={this.toggleCheckbox} />Terms and conditions
                                </label>
                            </div>
                            <div className="form-group">
                                <input className="btn btn-primary btn-block" type="button" value="Sign Up" onClick={ this.onRegisterClick } />
                            </div><a href="#" className="already" onClick={ this.onAlreadyHaveAnAccount }>You already have an account? Login here.</a>
                        </form>
                    </div>
                </div>
        );
    }
};
module.exports = RegisterComponent;