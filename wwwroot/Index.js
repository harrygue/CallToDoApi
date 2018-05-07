var uri = 'api/todo';

$(document).ready(function () {
    // Send an AJAX request
    $.getJSON(uri)
        .done(function (data) {
            // On success, 'data' contains a list of todos.
            $.each(data, function (key, item) {
                // Add a list item for the todoitem.
                $('<li>', { text: formatItem(item) }).appendTo($('#todos'));
            });
        });
});

function formatItem(item) {
    return item.id + ':         $' + item.name + ':         $' + item.isComplete;
}

function find() {
    var id = $('#todoId').val();
    $.getJSON(uri + '/' + id)
        .done(function (data) {
            $('#todoItem').text(formatItem(data));
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#todoItem').text('Error: ' + err);
        });
}