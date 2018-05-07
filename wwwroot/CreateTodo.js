// Create Todo
$("#createTodo").click(function (e) {
    // var isComplete = document.getElementById("iscomplete").value ? 
    $.ajax({
        contentType: 'application/json',
        type: "POST",
        url: "api/CallTodo",
        data: JSON.stringify({
            name: document.getElementById("name").value,
            isComplete: document.getElementById("iscomplete").value
        }),
        success: function (data, textStatus, jqXHR) {
            $("#showNewTodo").text("New Todo: " + jqXHR.responseText);
            $('#showData').text("data:" + data.name);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#showNewTodo").text(jqXHR.statusText);
        }
    });
});