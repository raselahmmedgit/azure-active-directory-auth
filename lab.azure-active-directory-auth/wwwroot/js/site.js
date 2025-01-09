var AppRoute = function () {

    var _redirect = function (redirectUrl) {

        let jwtToken = AppLocalStorage.GetJwtToken();
        if (jwtToken == undefined || jwtToken == null) {
            redirectUrl = '/Home/Index';
            window.location.href = (window.location.origin + redirectUrl);
            return;
        }
        else {
            if (redirectUrl == undefined || redirectUrl == null) {
                redirectUrl = '/Home/Index';
                window.location.href = (window.location.origin + redirectUrl);
                return;
            }

            window.location.href = (window.location.origin + redirectUrl + '?token=' + jwtToken);
            return;
        }
        
    };

    return {
        Redirect: _redirect
    };
}();

var AppLocalStorage = function () {

    var _get = function (key) {
        return localStorage.getItem(key);
    };

    var _set = function (key, value) {
        localStorage.removeItem(key);
        localStorage.setItem(key, value);
    };

    var _getJwtToken = function () {
        return localStorage.getItem('jwt_token');
    };

    var _setJwtToken = function (value) {
        localStorage.removeItem('jwt_token');
        localStorage.setItem('jwt_token', value);
    };

    return {
        Get: _get,
        Set: _set,
        GetJwtToken: _getJwtToken,
        SetJwtToken: _setJwtToken
    };
}();


var SignIn = function () {
    var initSignIn = function () {

        $("#btnSignInSubmit").on('click', function () {
            _submit();
        });

    };

    var _submit = function () {

        let dataObj = {
            UserEmail: $("#UserEmail").val(),
            UserPassword: $("#UserPassword").val()
        }

        //let authorization = ('Bearer ' + AppLocalStorage.GetJwtToken());

        $.ajax({
            type: 'POST',
            //url: '/Account/Login',
            url: '/api/Account/Login',
            //url: '/api/Account/LoginToken',
            data: JSON.stringify(dataObj),

            dataType: 'json',
            contentType: 'application/json',
            //headers: { "Authorization": authorization },
            beforeSend: function () {
                App.LoaderShow();
            },
            success: function (result) {
                //console.log(result);
                if (result != undefined || result != null) {

                    if (result.success == true) {
                        //console.log(result.message);
                        $('.message').addClass('text-success');
                        $('.message').html('');
                        $('.message').html(result.message);

                        console.log(result.data);
                        AppLocalStorage.SetJwtToken(result.data.token);

                        AppRoute.Redirect(result.redirectUrl);
                    }
                    else {
                        //console.log(result.message);
                        $('.message').addClass('text-danger');
                        $('.message').html('');
                        $('.message').html(result.message);
                    }

                    //window.location.reload();
                }
                //check null
            },
            complete: function (xhr, status) {
                App.LoaderHide();
            },
            error: function (req, status, error) {
                App.LoaderHide();
            }
        });

    };

    return {
        InitSignIn: initSignIn
    };
}();


var SignUp = function () {
    var initSignUp = function () {

        $("#btnSignUpSubmit").on('click', function () {
            _submit();
        });

    };

    var _submit = function () {

        let dataObj = {
            UserName: $("#UserName").val(),
            UserEmail: $("#UserEmail").val(),
            UserPassword: $("#UserPassword").val(),
            UserConfirmPassword: $("#UserConfirmPassword").val(),
            CardNumber: $("#CardNumber").val(),
            CardExpiration: $("#CardExpiration").val(),
            CardCvc: $("#CardCvc").val(),
            CardCountry: $("#CardCountry").val()
        }

        //let authorization = ('Bearer ' + AppLocalStorage.GetJwtToken());

        $.ajax({
            type: "POST",
            //url: "/Account/Register",
            url: "/api/Account/Register",
            data: JSON.stringify(dataObj),

            dataType: 'json',
            contentType: 'application/json',
            //headers: { "Authorization": authorization },
            beforeSend: function () {
                App.LoaderShow();
            },
            success: function (result) {
                //console.log(result);
                if (result != undefined || result != null) {

                    if (result.success == true) {
                        //console.log(result.message);
                        $('.message').addClass('text-success');
                        $('.message').html('');
                        $('.message').html(result.message);

                        console.log(result.data);
                        AppLocalStorage.SetJwtToken(result.data.token);

                        AppRoute.Redirect(result.redirectUrl);
                    }
                    else {
                        //console.log(result.message);
                        $('.message').html('');
                        $('.message').html(result.message);
                    }

                    //window.location.reload();
                }
                //check null
            },
            complete: function (xhr, status) {
                App.LoaderHide();
            },
            error: function (req, status, error) {
                App.LoaderHide();
            }
        });
    };

    return {
        InitSignUp: initSignUp
    };
}();


var WebCalculator = function () {
    var initWebCalculator = function () {

        const display = document.querySelector(".web-calc-display");
        const buttons = document.querySelectorAll("button");
        const specialChars = ["%", "*", "/", "-", "+", "="];
        let output = "";

        let memory = "";

        //Define function to calculate based on button clicked.
        const calculate = (btnValue) => {
            
            display.focus();
            if (btnValue === "=" && output !== "") {
                //If output has '%', replace with '/100' before evaluating.
                output = eval(output.replace("%", "/100"));
            } else if (btnValue === "C") {
                output = "";
            } else if (btnValue === "DEL") {
                //If DEL button is clicked, remove the last character from the output.
                output = output.toString().slice(0, -1);
            } else if (btnValue === "MC") {
                memory = "";
                output = "";
            } else if (btnValue === "MR") {
                if (memory === "") {
                    output = "";
                }
                else {
                    output = memory;
                }
                
            } else if (btnValue === "M+") {
                if (memory === "") {
                    memory = output;
                    output = "";
                }
                else {
                    output = Number(memory) + Number(output);
                    memory = output;
                }
            } else if (btnValue === "M-") {
                if (memory === "") {
                    memory = output;
                    output = "";
                }
                else {
                    output = Number(memory) - Number(output);
                    memory = output;
                }
            } else {
                //If output is empty and button is specialChars then return
                if (output === "" && specialChars.includes(btnValue)) return;
                output += btnValue;
            }
            display.value = output;
        };

        //Add event listener to buttons, call calculate() on click.
        buttons.forEach((button) => {
            //Button click listener calls calculate() with dataset value as argument.
            button.addEventListener("click", (e) => calculate(e.target.dataset.value));
        });

    };

    return {
        InitWebCalculator: initWebCalculator
    };
}();


var Member = function () {
    var initMember = function () {

        $("#lnkGetMembers").on('click', function () {
            _getMembers();
        });

    };

    var _getMembers = function () {

        let authorization = ('Bearer ' + AppLocalStorage.GetJwtToken());

        $.ajax({
            type: 'GET',
            url: '/Admin/Members',

            dataType: 'json',
            contentType: 'application/json',
            headers: { "Authorization": authorization },
            beforeSend: function () {
                App.LoaderShow();
            },
            success: function (result) {
                //console.log(result);
                if (result != undefined || result != null) {

                    if (result.success == true) {
                        //console.log(result.message);
                        console.log(result.data);
                    }
                    else {
                        //console.log(result.message);
                    }

                }
                //check null
            },
            complete: function (xhr, status) {
                App.LoaderHide();
            },
            error: function (req, status, error) {
                App.LoaderHide();
            }
        });

    };

    return {
        InitMember: initMember
    };
}();
