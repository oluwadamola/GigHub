var gigDetailsController = function () {
    var followingButton;
    var init = function() {
        $(".js-toggle-follow").click(toggleFollowing);

    }
    var toggleFollowing = function(e) {
        followingButton = $(e.target); 
        var followeeId = followingButton.attr("data-user-id");

        if (followingButton.hasClass("btn-default"))
            followingService.createFollowing(followeeId, done, fail);
        else
            followingService.deleteFollowing(followeeId, done, fail);
    };
    var done = function() {
        var text = (followingButton.text() === "Follow") ? "Following" : "Follow";

        followingButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function() {
        alert("Something failed!");
    };
    return {
        init: init
}
}(followingService);