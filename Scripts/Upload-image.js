function ShowImage(imageUploader, preViewImage)
{
    if (imageUploader.files && imageUploader.files[0]) {
        var render = new FileReader();
        render.onload = function (e) {
            $(preViewImage).attr('src', e.target.result);
        }
        render.readAsDataURL(imageUploader.files[0]);
    }
}