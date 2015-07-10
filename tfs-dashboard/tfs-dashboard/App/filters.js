var app = angular.module("tfsApp");

app.filter("workItemsState", function () {
    return function(items, state) {
        var filtered = [];
        angular.forEach(items, function(item) {
            if (angular.lowercase(item.Status) === angular.lowercase(state))
                filtered.push(item);
        });
        return filtered;
    };
});