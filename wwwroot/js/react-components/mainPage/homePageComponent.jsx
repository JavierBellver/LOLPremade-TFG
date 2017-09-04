class HomePage extends React.Component {
    render() {
        return (<div>
                    <div className="home-page">
                        <div className="container">
                            <div className="intro">
                                <h2 className="text-center">What can lolpremade offer to you?</h2>
                                <p className="text-center">This is some of the cool functionalities that Lolpremade offers to you</p>
                            </div>
                            <div className="row projects">
                                <div className="col-lg-4 col-sm-6 item">
                                    <img className="img-responsive" src="/images/find-players.jpg" />
                                    <h3 className="name">Find Other Players!</h3>
                                    <p className="description">Tired of not finding anyone to play with? Go to the<strong>Players</strong>section and look for other players according to your needs.</p>
                                </div>
                                <div className="col-lg-4 col-sm-6 item">
                                    <img className="img-responsive" src="/images/find-team.jpg" />
                                    <h3 className="name">Join a Team!</h3>
                                    <p className="description"><strong>Tired of Solo Quering?</strong> Wish you had a team with simiilar interests and objectives such as yours? Go to the Teams section and find your perfect team</p>
                                </div>
                                <div className="col-lg-4 col-sm-6 item">
                                    <img className="img-responsive" src="/images/find-tournaments.jpg" />
                                    <h3 className="name">Find tournaments to play in!</h3>
                                    <p className="description">Are you and your team looking for a challenge? Do you want to rise up on the scene and get some<strong>recognition and prices</strong>? Then check out our Tournaments section.</p>
                                </div>
                                <div className="col-lg-4 col-sm-6 item">
                                    <img className="img-responsive" src="/images/manage-team.jpg" />
                                    <h3 className="name">Run your team like a well oiled machine</h3>
                                    <p className="description">We know that is difficult getting to the top.<strong>But we&#39;re here to help!</strong>Manage your team&#39;s composition, find new talent, get new requests and prepare new training sessions!</p>
                                </div>
                                <div className="col-lg-4 col-sm-6 item">
                                    <img className="img-responsive" src="/images/organize-tournaments.jpg" />
                                    <h3 className="name">Organize your own tournaments!</h3>
                                    <p className="description"><strong>Did you always wondered about creating your own League of Legends tournaments either localy or online?</strong> Well wonder no more Lolpremade is the tool you need to reach for the teams and players your future tournament needs</p>
                                </div>
                                <div className="col-lg-4 col-sm-6 item">
                                    <img className="img-responsive" src="/images/reach-your-top.jpg" />
                                    <h3 className="name">Reach your personal and team's top</h3>
                                    <p className="description">Lolpremade is a tool to help you reach you and your team reach your peak on competitive League of Legends<strong>Let us help you in getting to the TOP</strong></p>
                                </div>
                            </div>
                        </div>
                    </div>
               </div>);
    }
}
module.exports = HomePage;