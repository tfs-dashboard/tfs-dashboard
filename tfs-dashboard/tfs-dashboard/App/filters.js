var app = angular.module('tfsApp')

app.filter('workItemsState', function () {
    return function (items, state) {
        var ItemState;
        switch (state) {
            case 1: ItemState = 'Proposed'; break;
            case 2: ItemState = 'Active'; break;
            case 3: ItemState = 'Resolved'; break;
        }
        var filtered = [];
        angular.forEach(items, function (item) {
            if (item.State == ItemState)
                filtered.push(item);
        })
        return filtered;
    }
})