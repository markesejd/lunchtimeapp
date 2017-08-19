var myApp = angular.module('myApp',[]);

myApp.controller('MainCtrl', ['$scope', '$http', function($scope, $http) {
  $scope.user = {
      firstName: '',
      lastName: '',
      email: ''
  };

  $scope.page = 'start';

  $scope.changePage = function(pageName) {
      $scope.page = pageName;
  }

  $scope.createAdventure = function() {
      $http.get({
        method: 'GET',
        url: '/api/room'
      }).then(function successCallback(response) {
          $scope.adventures.push(response)
          $scope.changePage('ongoing');
        }, function errorCallback(response) {
          alert('Error Creating Room');
        }
      );
  }
}]);