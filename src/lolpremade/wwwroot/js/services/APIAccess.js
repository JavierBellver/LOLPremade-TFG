function APIAccess() {
    this.GetToken = function (username, password) {
        var authForm = new FormData();
        authForm.append("username", username);
        authForm.append("password", password);

        return fetch('/api/token', {
            method: 'POST',
            body: authForm
        })
        .then(function (response) {
            if (response.ok)
                return response.json();
            else
                console.log('Request for token failed');
        })
        .catch(function (error) {
            console.log('Error with fetch operation');
        });
    };

    this.Get = function (resource, queryoptions) {
        var queryparameters = '';
        if (queryoptions != null) {
            queryparameters = '?'+Object.keys(queryoptions)
                .map(function (k)
                {
                    return encodeURIComponent(k) + '=' + encodeURIComponent(queryoptions[k])
                })
                .join('&');
        }
        return fetch('/api/' + resource + queryparameters, {
            method: 'GET'
        })
        .then(function (response) {
            if (response.ok)
                return response.json();
            else
                console.log("Error on GET request");
        })
        .catch(function (error) {
            console.log('Error with fetch operation');
        });
    };

    this.GetById = function(resource,id) {
        return fetch('/api/' + resource + '/' + id, {
            method: 'GET'
        })
        .then(function (response) {
            if (response.ok)
                return response.json();
            else
                console.log("Error on post request");
        })
        .catch(function (error) {
            console.log('Error with fetch operation');
        });
    };

    this.Post = function(resource,object) {
        return fetch('/api/' + resource, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: object
        })
        .then(function (response) {
            if (!response.ok)
                console.log("Error on post request");
            return response;
        })
        .catch(function (error) {
            console.log('Error with post operation');
        });
    };

    this.PostAuthorized = function (resource, object, token, queryoptions) {
        var queryparameters = '';
        if (queryoptions != null) {
            queryparameters = '?'+Object.keys(queryoptions)
                .map(function (k)
                {
                    return encodeURIComponent(k) + '=' + encodeURIComponent(queryoptions[k])
                })
                .join('&');
        }
        return fetch('/api/' + resource + queryparameters, {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            },
            body: object
        })
        .then(function (response) {
            if (!response.ok)
                throw new Error("Error on post request")
            return response;
        })
        .catch(function (error) {
            console.log('Error with fetch operation');
        });
    };

    this.Put = function (resource, id, object, token, queryoptions) {
        var queryparameters = '';
        if (queryoptions != null) {
            queryparameters = '?' + Object.keys(queryoptions)
                .map(function (k) {
                    return encodeURIComponent(k) + '=' + encodeURIComponent(queryoptions[k])
                })
                .join('&');
        }
        return fetch('/api/' + resource + '/' + id + queryparameters, {
            method: "PUT",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            },
            body: object
        })
        .then(function (response) {
            if (!response.ok)
                throw new Error("Error on put request")
            return response;
        })
        .catch(function (error) {
            console.log(error);
        });
    }

    this.Delete = function (resource, id, token) {
        return fetch('/api/' + resource + '/' + id, {
            method: "DELETE",
            headers: {
                'Authorization': 'Bearer '+token
            }
        })
        .then(function (response) {
            if (!response.ok)
                throw new Error("Error on delete request")
            return response;
        })
        .catch(function (error) {
            console.log(error)
        });
    }

    this.UploadTeamAvatar = function (avatar, token) {
        return fetch('/UploadTeamAvatar', {
            method: "POST",
            headers: {
                'Authorization': 'Bearer ' + token,
            },
            body: avatar
        })
        .then(function (response) {
            if (!response.ok)
                throw new Error("Error uploading team avatar")
            return response
        })
        .catch(function (error) {
            console.log(error)
        })
    }

    this.UploadUserAvatar = function (avatar, token) {
        return fetch('/UploadUserAvatar', {
            method: "POST",
            headers: {
                'Authorization': 'Bearer ' + token,
            },
            body: avatar
        })
        .then(function (response) {
            if (!response.ok)
                throw new Error("Error uploading users avatar")
            return response
        })
        .catch(function (error) {
            console.log(error)
        })
    }

    this.LeaveTeam = function (request, token) {
        return fetch('/LeaveTeam', {
            method: "POST",
            headers: {
                'Authorization': 'Bearer ' + token,
            },
            body: request
        })
        .then(function (response) {
            if (!response.ok)
                throw new Error("Error uploading users avatar")
            return response
        })
        .catch(function (error) {
            console.log(error)
        })
    }
}