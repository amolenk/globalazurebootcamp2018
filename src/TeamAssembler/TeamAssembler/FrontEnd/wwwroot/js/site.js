var app = angular.module('TeamAssemblerApp', ['ui.bootstrap']);
app.run(function () { });

app.controller('TeamAssemblerController', ['$rootScope', '$scope', '$http', '$timeout', function ($rootScope, $scope, $http, $timeout) {

    $scope.selectedMembers = {};

    $scope.init = function () {
        $scope.refresh();
        $scope.getMembers();
    }

    $scope.refresh = function () {
        $http.get('api/Teams?c=' + new Date().getTime())
            .then(function (response, status) {
                $scope.teams = response.data;
            }, function (response, status) {
                $scope.teams = undefined;
            });
    };

    $scope.getMembers = function () {
        $http.get('api/Members')
            .then(function (response, status) {
                $scope.members = response.data;
            }, function (response, status) {
                $scope.members = undefined;
            });
    };

    $scope.assemble = function () {
        var members = [];
        angular.forEach($scope.selectedMembers, function (selected, member) {
            if (selected) {
                members.push(member);
            }
        });
        var request = JSON.stringify({
            name: $scope.teamName,
            members: members
        });
        $http.put('api/Teams/' + $scope.teamName, JSON.stringify(members), {
                headers: { 'Content-Type': 'application/json' }
            })
            .then(function (response, status) {
                $scope.refresh();
                $scope.teamName = '';
                $scope.selectedMembers = [];
            });
    };

    $scope.remove = function (name) {
        $http.delete('api/Teams/' + name)
            .then(function (response, status) {
                $scope.refresh();
            })
    };
}]);