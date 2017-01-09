var AttendanceService = function () {
    var creatAttendances = function (gigId, done, fail) {
        $.post("/api/attendances", { gigId: gigId })
    .done(done)
    .fail(fail);
    };
    var deleteAttendances = function (gigId, done, fail) {
        $.ajax({
            url: "/api/attendances/" + gigId,
            method: "DELETE"
        })
     .done(done)
     .fail(fail);

    };
    return {
        createAttendances: creatAttendances,
        deleteAttendances: deleteAttendances
    }
}();