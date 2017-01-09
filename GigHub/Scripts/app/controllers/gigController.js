
var gigController = function (attendanceServices) {

    var button;
    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
        //$(".js-toggle-attendance").click(toggleAttendance);
    };

    var toggleAttendance = function(e) {
        button = $(e.target);

        var gigId = button.attr("data-gig-id");
        if (button.hasClass("btn-default"))
            attendanceServices.createAttendances(gigId, done, fail);
        else
            attendanceServices.deleteAttendances(gigId, done, fail);


    };
    var done = function() {
        var text = (button.text() === "Going") ? "Going?" : "Going";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };
    var fail = function() {
        alert("Something failed");
    };
    return {
        init: init
    };
}(AttendanceService);

