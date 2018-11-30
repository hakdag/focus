import { Directive, ElementRef, Input } from '@angular/core';
declare var jQuery: any;
declare var Messenger: any;

@Directive({
    selector: '[messenger-box]'
})

export class MessengerBox {
    $el: any;

    constructor() {
        this.render();
    }

    initializationCode(): void {
        /* tslint:disable */
        (function (): void {
            let $, flatMessage, spinnerTemplate,
                __hasProp = {}.hasOwnProperty,
                __extends = function (child, parent): any { for (let key in parent) { if (__hasProp.call(parent, key)) { child[key] = parent[key]; } } function ctor(): void { this.constructor = child; } ctor.prototype = parent.prototype; child.prototype = new ctor(); child.__super__ = parent.prototype; return child; };

            spinnerTemplate = '<div class="messenger-spinner">\n    <span class="messenger-spinner-side messenger-spinner-side-left">\n        <span class="messenger-spinner-fill"></span>\n    </span>\n    <span class="messenger-spinner-side messenger-spinner-side-right">\n        <span class="messenger-spinner-fill"></span>\n    </span>\n</div>';
            /* tslint:enable */
            flatMessage = (function (_super): any {

                __extends(flatMessage, _super);

                function flatMessage(): any {
                    /* tslint:disable */
                    return flatMessage['__super__'].constructor.apply(this, arguments);
                    /* tslint:enable */
                }

                flatMessage.prototype.template = function (opts): any {
                    let $message;
                    /* tslint:disable */
                    $message = flatMessage['__super__'].template.apply(this, arguments);
                    /* tslint:enable */
                    $message.append(jQuery(spinnerTemplate));
                    return $message;
                };

                return flatMessage;
                /* tslint:disable */
            })(window['Messenger'].Message);

            window['Messenger'].themes.air = {
                Message: flatMessage
            };
            /* tslint:enable */
        }).call(window);
    }

    render(): void {
        this.initializationCode();
        let theme = 'air';

        jQuery.globalMessenger({ extraClasses: 'messenger-fixed messenger-on-top', theme: theme });
        Messenger.options = { extraClasses: 'messenger-fixed messenger-on-top', theme: theme };
    }

    showErrorMessage(message, retryFunc): void {
        if (retryFunc) {
            var msg = Messenger().post({
                message: message,
                type: 'error',
                actions: {
                    retry: {
                        label: "Try again",
                        action: function () {
                            retryFunc();
                            msg.hide();
                        }
                    },
                    cancel: {
                        label: "Cancel",
                        action: function () {
                            msg.hide()
                        }
                    }
                }
            });
        } else {
            var msg = Messenger().post({
                message: message,
                type: 'error',
                actions: {
                    cancel: {
                        label: "Ok",
                        action: function () {
                            msg.hide()
                        }
                    }
                }
            });
        }
    }

    showSuccessMessage(): void {
        Messenger().post({
            message: 'Showing success message was successful!',
            type: 'success',
            showCloseButton: true
        });
    }

    showInfoMessage(): void {
        let msg = Messenger().post({
            message: 'Launching thermonuclear war...',
            actions: {
                cancel: {
                    label: 'cancel launch',
                    action: function (): any {
                        return msg.update({
                            message: 'Thermonuclear war averted',
                            type: 'success',
                            actions: false
                        });
                    }
                }
            }
        });
    }
}
