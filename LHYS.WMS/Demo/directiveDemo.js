//出入库类型指令 
//调用：
//<inouttypeselect inouttypes="InOutTypes" inputtypename="info.InputType" inputtypeid="info.InputTypeId" current="currentInOutType" ></inouttypeselect>

//加载初值：
for (var i = 0; i < $scope.InOutTypes.length; i++) {
    if ($scope.InOutTypes[i].Id == $scope.info.InputTypeId) {
        $scope.currentInOutType = $scope.InOutTypes[i];
    }
}

myApp.directive('inouttypeselect', function () {
    return {
        restrict: 'E',
        replace: true,
        scope: {
            inouttypes: '=', //选项列表
            inputtypename: '=',//里面存的ID
            inputtypeid: '=',//外面存的ID
            current: '=' //当前选择的
        },
        template: '<select class="form-control" ng-options=" InOutType.Name for InOutType in inouttypes" ng-model="current" ng-change="change()"  required></select>',
        link: function (scope, element, attrs) {
            scope.change = function () {
                scope.inputtypename = scope.current.Name;
                scope.inputtypeid = scope.current.Id;
            }
        }
    }
});