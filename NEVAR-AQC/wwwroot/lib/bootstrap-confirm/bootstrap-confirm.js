; (function ($) {
    'use strict';
    $.confirm = function (options) {
        if ($.fn.modal === undefined)
            throw new Error('bootstrap-confirm JavaScript requires Bootstrap.js');

        var settigns = $.extend({
            onOk: function () { },
            onCancel: function () { }
        }, $.confirm.defaults, options),
            dialogClose = '<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>',
            buttonOk = '<button type="button" class="btn btn-sm btn-' + settigns.templateOk + '" data-confirm="Ok">' + settigns.labelOk + '</button>',
            buttonCancel = '<button type="button" class="btn btn-sm btn-' + settigns.templateCancel + '" data-confirm="Cancel">' + settigns.labelCancel + '</button>',
            dialogFooter = '<div class="modal-footer pt-2 pb-2">' + (settigns.buttonCancel ? buttonCancel : '') + (settigns.buttonOk ? buttonOk : '') + '</div>',
            $dialog = $('<div class="modal fade" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">' +
                '<div class="modal-dialog modal-dialog-centered modal-sm" role="document">' +
                '<div class="modal-content">' +
                '<div class="modal-header font-weight-bold pt-2 pb-2 bg-' + settigns.template + '">' +
                '<span class="modal-title">' +
                (!settigns.buttonOk && !settigns.buttonCancel ? dialogClose : '') +
                '<i class="' + settigns.titleIcon + '"></i> ' + settigns.title + '</span>' +
                '</div>' +
                '<div class="modal-body">' + settigns.message + '</div>' +
                (settigns.buttonOk || settigns.buttonCancel ? dialogFooter : '') +
                '</div>' +
                '</div>'
            );

        $dialog.on('hidden.bs.modal', function (event) {
            $(this).remove();
            $('body').addClass('modal-open');
        });

        $dialog.on('shown.bs.modal', function (event) {
            $(this).next('.modal-backdrop').css('z-index', 2040);
        });

        $dialog.find('.modal-header').css({
            'color': '#fff',
        });

        $dialog.find('button[data-confirm="Ok"]').click(function (event) {
            event.preventDefault();
            $dialog.modal('hide');
            settigns.onOk.call(this);
        });

        $dialog.find('button[data-confirm="Cancel"]').click(function (event) {
            event.preventDefault();
            $dialog.modal('hide');
            settigns.onCancel.call(this);
        });

        $dialog.css('z-index', 2050);
        $dialog.appendTo('body');
        $dialog.modal('show');
    };

    $.confirm.defaults = {
        message: 'You message',
        buttonOk: true,
        buttonCancel: true,
        template: 'primary',
        title: 'Hệ thống',
        titleIcon: 'fa fa-question-circle',
        labelOk: 'Xác nhận',
        labelCancel: 'Hủy',
        templateOk: 'primary',
        templateCancel: 'secondary'
    };
})(jQuery);