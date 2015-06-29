var app = angular.module('tfsApp')

app.filter('workItemsState', function () {
    return function (items, state) {
        var ItemState;
        switch (state) {
            case 1: ItemState = 'Backlog'; break;
            case 2: ItemState = 'In work'; break;
            case 3: ItemState = 'Waiting for test'; break;
            case 4: ItemState = 'In test'; break
            case 5: ItemState = 'Waiting for release'; break;
        }
        var filtered = [];
        angular.forEach(items, function (item) {
            if (item.Status == ItemState)
                filtered.push(item);
        })
        return filtered;
    }
})