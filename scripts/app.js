var myApp = angular.module('myApp',[]);

myApp.controller('MainCtrl', ['$scope', function($scope) {
  $scope.user = {
      firstName: '',
      lastName: '',
      email: ''
  };

  $scope.page = 'start';

  $scope.changePage = function(pageName) {
      $scope.page = pageName;
  }
}]);