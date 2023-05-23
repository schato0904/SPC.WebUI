//Adjust image size
//needs jquery
//put a #grid and #resizeme on your html and call $('#resizeme').resizeimg() 
$.fn.resizeimg = function () {
    // 전체화면 사이즈에 맞출경우
    ////Make sure the image stays center in the window
    //$(this).children().css('left', (browserwidth - $(this).width()) / 2);
    //$(this).children().css('top', (browserheight - $(this).height()) / 2);

    //var width = $(this).width();
    //var height = $(this).height();
    //var parentWidth = $(this).parent().width();
    //var parentHeight = $(this).parent().height();
    
    //alert(parentWidth);
    //alert(parentHeight);

    //if (width / parentWidth > height / parentHeight) {
    //    alert('w');
    //    newWidth = parentWidth;
    //    newHeight = newWidth / width * height;
    //}
    //else {
    //    alert('h');
    //    newHeight = parentHeight;
    //    newWidth = newHeight / height * width;
    //}
    //margin_top = (parentHeight - newHeight) / 2;
    //margin_left = (parentWidth - newWidth) / 2;

    //alert(parentWidth + ' : ' + newWidth);
    //alert(parentHeight + ' : ' + newHeight);
    //$(this).css({
    //    'height': newHeight + 'px',
    //    'width': newWidth + 'px'
    //});

    // 넓이 값만 화면 사이즈에 맞출경우
    var width = $(this).width();
    var height = $(this).height();
    var parentWidth = $(window).width();

    if (width > parentWidth) {
        newWidth = parentWidth - 10;
        newHeight = newWidth / width * height;
    }
    else {
        newHeight = width;
        newWidth = height;
    }

    $(this).css({
        'height': newHeight + 'px',
        'width': newWidth + 'px'
    });
};