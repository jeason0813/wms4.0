//json日期格式转换为正常格式
function jsonDateFormat(jsonDate, type) {
    try {
        var date = new Date(parseInt(jsonDate.replace("/Date(", "").replace(")/", ""), 10));
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        var hours = date.getHours();
        var minutes = date.getMinutes();
        var seconds = date.getSeconds();
        var milliseconds = date.getMilliseconds();
        var date = date.getFullYear() + "-" + month + "-" + day;
        if (type == 1) {
            return date;
        } else {
            return new Date(date);
        }
    } catch (ex) {
        return "";
    }
}
//主程序
var myApp = angular.module('myApp', []);
//出入库类型选择下拉菜单
myApp.directive('inouttypeselect', function () {
    return {
        restrict: 'E',
        replace: true,
        scope: {
            inouttypes: '=', //选项列表
            inputtypename: '=',//里面存的ID
            inputtypeid: '=',//外面存的ID
            current: '=',//当前选择的
            filterstr:'@'//过滤条件
        },
        template: '<select class="form-control input-sm" ng-options=" InOutType.Name for InOutType in inouttypes |filter:filterstr" ng-model="current" ng-change="change()"  required><option value=""></option></select>',
        link: function (scope, element, attrs) {
            scope.change = function () {
                scope.inputtypename = scope.current.Name;
                scope.inputtypeid = scope.current.Id;
            }
        }
    }
});