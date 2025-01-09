
var appMessage = {
    Error: 'We are facing some problem while processing the current request. Please try again later.',
    NotFound: 'Requested object not found.',
    SaveSuccess: 'Save successfully.',
    UpdateSuccess: 'Update successfully.',
    DeleteSuccess: 'Delete successfully.'
};

var App = function () {
    var appAreaName = {
        Admin: 'Admin'
    };
    var appCopyUrl = function () {
        $(".app-copy-url").select();
        document.execCommand("copy");
    };
    var loaderShow = function () {
        $("#appPreLoader").addClass("appPreLoaderLight");
        $("#appPreLoader").fadeIn(100);
    };

    var loaderHide = function () {
        $("#appPreLoader").remove("appPreLoaderLight");
        $("#appPreLoader").fadeOut(100);
    };

    var sendAjaxRequest = function (url, data, isPost, callback, isAsync, isJson, target) {
        isJson = typeof (isJson) == 'undefined' ? true : isJson;
        var contentType = (isJson) ? "application/json" : "text/plain";
        var dataType = (isJson) ? "json" : "html";
        if (!isAsync) {
            App.LoaderShow();
        }

        return $.ajax({
            type: isPost ? "POST" : "GET",
            url: url,
            data: isPost ? JSON.stringify(data) : data,
            contentType: contentType,
            dataType: dataType,
            beforeSend: function (xhr) {
                App.LoaderShow();
            },
            success: function (successData) {
                if (!isAsync) {
                    App.LoaderHide();
                }
                return typeof (callback) == 'function' ? callback(successData) : successData;
            },
            complete: function (xhr, status) {
                App.LoaderHide();
            },
            error: function (exception) {
                return false;
            },
            async: isAsync
        });
    };

    var arrayToTree = function (arr, parent) {
        //arr.sort(function (a, b) { return parseInt(b.Level) - parseInt(a.Level) });
        var out = [];
        for (var i in arr) {
            if (arr[i].ParentId == parent) {
                var data = new Object();
                data.text = arr[i].Name;
                if (arr[i].Level == 3) {
                    data.id = arr[i].Id;
                } else {
                    var children = arrayToTree(arr, arr[i].Id);
                    if (children.length) {
                        data.children = children;
                    }
                }
                out.push(data);
            }
        }
        return out;
    };

    var loadDropdown = function (targetDropdown, dataSourceUrl, filterByValue) {

        App.SendAjaxRequest(dataSourceUrl, filterByValue, false, function (options) {
            var optionHtml = '';

            if ($.isArray(options) && (options.length > 0)) {

                $(options).each(function (index, option) {
                    if (option.selected == true) {
                        optionHtml += '<option value="' + option.value + '" selected>' + option.text + '</option>';
                    }
                    else {
                        optionHtml += '<option value="' + option.value + '" >' + option.text + '</option>';
                    }
                });

            }

            $('#' + targetDropdown).html(optionHtml);

        }, true);

        $('#' + targetDropdown).val(0);

    };

    var toUpperCase = function (strText) {

        var _strText;
        _strText = strText.toLowerCase().replace(/\b[a-z]/g, function (letter) {
            return letter.toUpperCase();
        });

        return _strText;
    };

    var displayLength = function () {
        var _displayLength = 10;
        return _displayLength;
    };

    var actionHandler = function () {

    };

    var initializeApp = function () {
        actionHandler();
    };

    var getAjax = function (getUrl, param) {

        $.getJSON(getUrl, param).done(function (data) {
            return data;
        }).fail(function (jqxhr, textStatus, error) {
            var msg = textStatus + ", " + error;
            toastr['error'](msg, "Error !");
        });

    };

    var toCamelCase = function (str) {
        return str.replace(/(?:^\w|[A-Z]|\b\w)/g, function (word, index) {
            return index == 0 ? word.toLowerCase() : word.toUpperCase();
        }).replace(/\s+/g, '');
    };


    //-----------------------------------------------------
    //start Ajax Get Methods

    var ajaxJsonGet = function (getUrl) {

        $.ajax({
            url: getUrl,
            type: 'GET',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                OpenAppProgressModal();
            },
            success: function (result) {
                var messageType = result.messageType;
                var messageText = result.messageText;
                LoadAppMessageModal(messageType, messageText);
            },
            error: function (objAjaxRequest, strError) {
                var respText = objAjaxRequest.responseText;
                var messageText = respText;
                LoadErrorAppMessageModalWithText(messageText);
            }

        });

    }

    var ajaxJsonGetWithParam = function (getUrl, paramValue) {

        $.ajax({
            url: getUrl,
            type: 'GET',
            dataType: 'json',
            data: paramValue,
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                OpenAppProgressModal();
            },
            success: function (result) {
                var messageType = result.messageType;
                var messageText = result.messageText;
                LoadAppMessageModal(messageType, messageText);
            },
            error: function (objAjaxRequest, strError) {
                var respText = objAjaxRequest.responseText;
                var messageText = respText;
                LoadErrorAppMessageModalWithText(messageText);
            }

        });

    }

    //end Ajax Get Methods
    //-----------------------------------------------------

    //-----------------------------------------------------
    //start Ajax Post Methods

    var ajaxJsonPost = function (postUrl) {

        $.ajax({
            url: postUrl,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                OpenAppProgressModal();
            },
            success: function (result) {
                var messageType = result.messageType;
                var messageText = result.messageText;
                LoadAppMessageModal(messageType, messageText);
            },
            error: function (objAjaxRequest, strError) {
                var respText = objAjaxRequest.responseText;
                var messageText = respText;
                LoadErrorAppMessageModalWithText(messageText);
            }

        });

    }

    var ajaxJsonPostForDelete = function (postUrl) {

        $.ajax({
            url: postUrl,
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                OpenAppProgressModal();
            },
            success: function (result) {
                var messageType = result.messageType;
                var messageText = result.messageText;
                LoadAppMessageModal(messageType, messageText);
                DataTableRefreshInIndexPage();
            },
            error: function (objAjaxRequest, strError) {
                var respText = objAjaxRequest.responseText;
                var messageText = respText;
                LoadErrorAppMessageModalWithText(messageText);
            }

        });

    }

    var ajaxJsonPostWithParam = function (postUrl, paramValue) {

        $.ajax({
            url: postUrl,
            type: 'POST',
            dataType: 'json',
            data: paramValue,
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                OpenAppProgressModal();
            },
            success: function (result) {
                var messageType = result.messageType;
                var messageText = result.messageText;
                LoadAppMessageModal(messageType, messageText);
            },
            error: function (objAjaxRequest, strError) {
                var respText = objAjaxRequest.responseText;
                var messageText = respText;
                LoadErrorAppMessageModalWithText(messageText);
            }

        });

    }

    var ajaxJsonPostForDeleteWithParam = function (postUrl, paramValue) {

        $.ajax({
            url: postUrl,
            type: 'POST',
            dataType: 'json',
            data: paramValue,
            contentType: 'application/json; charset=utf-8',
            beforeSend: function () {
                OpenAppProgressModal();
            },
            success: function (result) {
                var messageType = result.messageType;
                var messageText = result.messageText;
                LoadAppMessageModal(messageType, messageText);
                DataTableRefreshInIndexPage(); //Have to add index page
            },
            error: function (objAjaxRequest, strError) {
                var respText = objAjaxRequest.responseText;
                var messageText = respText;
                LoadErrorAppMessageModalWithText(messageText);
            }

        });

    }

    //end Ajax Post Methods
    //-----------------------------------------------------

    return {
        Init: initializeApp,
        LoaderShow: loaderShow,
        LoaderHide: loaderHide,

        SendAjaxRequest: sendAjaxRequest,
        LoadDropdown: loadDropdown,

        DisplayLength: displayLength,

        GetAjax: getAjax,

        AjaxJsonGet: ajaxJsonGet,
        AjaxJsonGetWithParam: ajaxJsonGetWithParam,
        AjaxJsonPost: ajaxJsonPost,
        AjaxJsonPostForDelete: ajaxJsonPostForDelete,
        AjaxJsonPostWithParam: ajaxJsonPostWithParam,
        AjaxJsonPostForDeleteWithParam: ajaxJsonPostForDeleteWithParam,
        ToCamelCase: toCamelCase,
        AppAreaName: appAreaName,
        AppCopyUrl: appCopyUrl
    };
}();

$(document).ready(function () {

    App.Init();

});