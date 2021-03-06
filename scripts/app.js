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

  //Array of adventures
  $scope.adventures = [];
  $scope.currentAdventure = {};

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

  $scope.getAdventure = function(id) {
      $http.get({
        method: 'GET',
        url: '/api/room/' + id
      }).then(function successCallback(response) {
          $scope.currentAdventure = response;
          $scope.changePage('current');
        }, function errorCallback(response) {
          alert('Error Locating Room');
        }
      );
  }

  $scope.getAdventures = function() {
      $http.get({
        method: 'GET',
        url: '/api/rooms/'
      }).then(function successCallback(response) {
          $scope.adventures = response;
          $scope.changePage('ongoing');
        }, function errorCallback(response) {
          alert('Error Locating Rooms');
        }
      );
  }

  $scope.addVenue = function(id, venue) {
      $http.post({
        method: 'POST',
        url: 'api/room/' + id + '/venue/' + venue,
        headers: {
            "X-UserId": $scope.user.email
        }
      }).then(function successCallback(response) {
          //Good to go. 
        }, function errorCallback(response) {
          alert('Error Adding Venue');
        }
      );
  }

  $scope.voteVenue = function(id, venue) {
      $http.post({
        method: 'POST',
        url: 'api/room/' + id + '/venue/' + venue + '/vote',
        headers: {
            "X-UserId": $scope.user.email
        }
      }).then(function successCallback(response) {
          //Good to go. 
        }, function errorCallback(response) {
          alert('Error Voting Venue');
        }
      );
  }

  $scope.addTime = function(id, time) {
      $http.post({
        method: 'POST',
        url: 'api/room/' + id + '/time/' + time.replace(':', '_'),
        headers: {
            "X-UserId": $scope.user.email
        }
      }).then(function successCallback(response) {
          //Good to go. 
        }, function errorCallback(response) {
          alert('Error Adding Time');
        }
      );
  }

  $scope.voteTime = function(id, time) {
      $http.post({
        method: 'POST',
        url: 'api/room/' + id + '/time/' + time.replace(':', '_') + '/vote',
        headers: {
            "X-UserId": $scope.user.email
        }
      }).then(function successCallback(response) {
          //Good to go. 
        }, function errorCallback(response) {
          alert('Error Voting Time');
        }
      );
  }

  $scope.addDriver = function(id) {
      $http.post({
        method: 'POST',
        url: 'api/room/' + id + '/driver',
        headers: {
            "X-UserId": $scope.user.email
        }
      }).then(function successCallback(response) {
          //Good to go. 
        }, function errorCallback(response) {
          alert('Error Adding Driver');
        }
      );
  }

}]);