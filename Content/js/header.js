var notification = document.getElementById("notification-event");
var user = document.getElementById("user-event");
var popup_notification = document.getElementsByClassName("notification__popup")[0];
var popup_user = document.getElementsByClassName("user__popup")[0];
var overlay = document.getElementById("overlay")

notification.addEventListener("click", () => {
    popup_notification.classList.toggle("active")
    overlay.classList.toggle("active")
});

user.addEventListener("click", () => {
    popup_user.classList.toggle("active")
    overlay.classList.toggle("active")
});

overlay.addEventListener("click", () => {
    popup_notification.classList.remove("active")
    popup_user.classList.remove("active")
    overlay.classList.remove("active")

})