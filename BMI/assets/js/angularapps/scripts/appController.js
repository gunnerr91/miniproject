ngBMIApp.controller("ngBMIAppController", function ($scope, ngBMIService) {

    $scope.init = function () {
        $scope.load();
    }

    $scope.load = function () {
        ngBMIService.load()
            .then(function (response) {
                $scope.tabledata = response.data;
            });
    }

    $scope.calculate = function () {
        $scope.BMIModel = {
            Name: $scope.name,
            Height: $scope.height,
            Weight: $scope.weight,
            Unit: $scope.unit
        };

        ngBMIService
            .add($scope.BMIModel)
            .then(function (response) {
                $scope.bmivalue = response.data;
                ngBMIService.load()
                    .then(function (response) {
                        $scope.tabledata = response.data;
                    });
            });
        

    }
    

});
