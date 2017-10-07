ngBMIApp.service('ngBMIService', ["$http", function ($http) {
    return {
        load: function () {
            return $http.get("/webapi/bmi/");
        },

        add: function (jsonfiedData) {
            return $http.post("/webapi/bmi", jsonfiedData);
        }
    }
}]);