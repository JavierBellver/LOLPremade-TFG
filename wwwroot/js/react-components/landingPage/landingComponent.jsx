import { Link } from 'react-router-dom'

class LandingComponent extends React.Component{
    render() {
        return (
            <div>
                <div className="jumbotron hero">
                    <div className="container">
                        <div className="row">
                            <div className="col-md-4 col-md-push-7 phone-preview"></div>
                            <div className="col-md-6 col-md-pull-3 get-it">
                                <h1>LoLPremade </h1>
                                <p>You will need allies to perdure on the Summoners Rift. Register Now!</p>
                                <p>
                                    <Link to="/register"><button className="btn btn-primary btn-lg" type="button" name="access-register">Register</button></Link>
                                    <Link to="/login"><button className="btn btn-primary btn-lg" type="button" name="access-login">Login</button></Link>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            <section className="testimonials">
                <h2 className="text-center">An indispensable tool for every summoner</h2>
                <blockquote>
                    <p>Find other players! Get a team going and fight to get to the top of the competitive League of Leguends scene.</p>
                </blockquote>
            </section>
            <section className="features">
                <div className="container">
                    <div className="row">
                        <div className="col-md-6">
                            <h2>A whole solution for League of Legends Players, Teams, Tournament Organizers awaits you</h2>
                            <p>Also with an easy to use API service</p>
                        </div>
                    </div>
                </div>
            </section>
            <footer className="site-footer">
                <div className="container">
                    <div className="row">
                        <div className="col-sm-6">
                            <h5>Lolpremade © 2017</h5>
                        </div>
                        <div className="col-sm-6 social-icons"><a href="#"><i className="fa fa-facebook"></i></a><a href="#"><i className="fa fa-twitter"></i></a></div>
                    </div>
                </div>
            </footer>
            </div>
        );
    }
};
module.exports = LandingComponent;