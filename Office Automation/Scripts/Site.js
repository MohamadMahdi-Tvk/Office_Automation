﻿function imagePreview(input) {
    if (input.files && input.files[0]) {
        var filerdr = new FileReader();
        filerdr.onload = function (e) {
            jQuery('#newImagePreview').attr('src', e.target.result);
        }
        filerdr.readAsDataURL(input.files[0]);
    }
}