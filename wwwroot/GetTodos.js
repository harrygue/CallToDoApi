// get all Todo's
$('#getTodo').click(function (e) {
    $('#getTodoResult').val("");
    $.ajax({
        contentType: 'application/json',
        type: "GET",
        url: "api/CallTodo",
        success: function (data, textStatus, jqXHR) {
            data.forEach(function (todo) {
                $('#todoTable tbody').append("<tr>", formatRow(todo));
                
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('#getTodoResult').val(jqXHR.statusText);
        }
    });
});
// get Todo by Id

    $("#btnTodoById").click(function (e) {
        $("#showTodoById").val();
        var Id = $("#id").val();
        $.ajax({
            contentType: 'application/json',
            type: "GET",
            url: "api/calltodo/" + Id,
            success: function (data, textStatus, jqXHR) {
                $('#showTodoById').text(data.id + " | " + data.name + " | " + data.isComplete);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('#showTodoById').text(jqXHR.statusText);
            }
        });
    });


function formatRow(rowItem) {
    localStorage.setItem("todoId", rowItem.id);
    localStorage.setItem("todoName", rowItem.Name);
    localStorage.setItem("todoIsComplete", rowItem.isComplete);
    return $('<tr>').append('<td>', rowItem.id)
        .append('<td>', rowItem.name)
        .append('<td>', rowItem.isComplete)
        .append('<td>', "Select")
        .click(function () {
            $(this).css("color", "blue");
            $('#showTodoById').text(rowItem.id + " | " + rowItem.name + " | " + rowItem.isComplete);
        })
        .append('<td>', '<a href="http://localhost:57260/UpdateTodo.html">Edit</a>')
        .click(function () {
            localStorage.setItem("todoId", rowItem.id);
            localStorage.setItem("todoName", rowItem.name);
            localStorage.setItem("todoIsComplete", rowItem.isComplete);
        })
        .append('<td>', '<a href="http://localhost:57260/DeleteTodo.html">Delete</a>')
        .click(function () {
            localStorage.setItem("todoId", rowItem.id);
            localStorage.setItem("todoName", rowItem.name);
            localStorage.setItem("todoIsComplete", rowItem.isComplete);
        });
}

