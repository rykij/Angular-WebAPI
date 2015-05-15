
(function () {
    'use strict';

    angular.module('form')
    .controller('LoginController', LoginController);

    function LoginController($scope, RegisterInfoService) {
        var viewModel = this;

        viewModel.login =
        {
            UserName: '',
            Password: '',
            ConfirmPassword: '',
            rememberMe: false
        };

        viewModel.message = '';
        viewModel.error = '';

        viewModel.checkUserCredentials = function (isValid) {
            if (isValid) {
                viewModel.error = '';
                viewModel.userInfo = {
                    UserName: viewModel.login.UserName,
                    Password: viewModel.login.Password,
                };
                var promise = RegisterInfoService.getRegisterInfo(viewModel.userInfo);
                promise.then(function (response) {
                    viewModel.message = response;
                });
            }
            else {
                viewModel.message = '';
                viewModel.error = "Check required fields";
            }
        };
    };
})();

