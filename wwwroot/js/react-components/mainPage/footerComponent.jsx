class FooterComponent extends React.Component {
    render() {
        return (
            <div className="footer-clean">
                    <footer>
                        <div className="container">
                            <div className="row">
                                <div className="col-md-3 col-sm-4 item">
                                    <h3>About</h3>
                                    <ul>
                                        <li><a href="/about">The App</a></li>
                                        <li><a href="#">Lolpremade Team</a></li>
                                        <li><a href="#">The API</a></li>
                                    </ul>
                                </div>
                                <div className="col-md-3 item social pull-right">
                                    <a href="#"><i className="icon ion-social-facebook"></i></a><a href="#"><i className="icon ion-social-twitter"></i></a>
                                    <p className="copyright">LOLPremade © 2017</p>
                                </div>
                            </div>
                        </div>
                    </footer>
                </div>
        );
    }
};
module.exports = FooterComponent;