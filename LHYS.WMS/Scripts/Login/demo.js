/**
 * Particleground demo
 * @author Jonathan Nicol - @mrjnicol
 */

$(document).ready(function() {
    $('#particles').particleground({
    dotColor: '#5cbdaa',
    lineColor: '#5cbdaa'
       
  });
    $('#intro').css({
        'margin-top': -($('#login').height() / 2)
  });
});