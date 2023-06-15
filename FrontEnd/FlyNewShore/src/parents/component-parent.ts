import { AfterViewInit, OnDestroy } from '@angular/core';
import swal from 'sweetalert';
import { CommondParent } from './commond-parent';

export abstract class ComponentParent extends CommondParent  {

    private static statusInit: boolean = false;
    private static _loading: boolean = false;
    private static loadingTimes: number = 0;
    //static globalToast: ToastrService;

    constructor() {
        super();
    }

    // ngAfterViewInit() {
    //     /*setTimeout(() => {
    //         ComponentParent.statusInit = true;
    //     }, 0);*/
    // }

    // ngOnDestroy() {
    //     //ComponentParent.statusInit = false;
    //     //window.onbeforeunload = null;
    // }

    public static get loading(): boolean {
        return this._loading;
    }

    public static set loading(value: boolean) {
        if (value === null) {
            ComponentParent.loadingTimes = 0;
            ComponentParent._loading = false;
        } else {
            ComponentParent.loadingTimes += (value ? 1 : -1);
        }
        if (!this.statusInit) {
            return;
        }
        ComponentParent._loading = ComponentParent.loadingTimes !== 0;
    }

    get htmlLoading() {
        return ComponentParent.loading;
    }

    // logs
    logHandled(message: any) {
        console.log(message);
    }

    errorHandled(message: any, showError: boolean = true) {
        if (showError) {
            //this.alertError();
        }
    }

    // alerts
    private alert(text: string, title: string, icon:any) {
        swal({
            title: title ? title : text!,
            text: title ? text : text!,
            icon: icon
        });
    }

    alertOk(text: string, title: string) {
        this.alert(text, title, 'success');
    }


    alertOkProm(text: string = 'Proceso realizado con éxito.', title: string) {
        return new Promise((res) => {
            swal({
                title: title,
                text: text,
                icon: 'success'
            }).then((ok) => {
                    res(true);
            });
        });
    }
    alertInfo(text: string, title: string) {
        this.alert(text, title, 'info');
    }

    alertWarning(text: string, title: string) {
        this.alert(text, title, 'warning');
    }

    alertError(text: string, title: string) {
        this.alert(text, title, 'error');
    }
    
    // confirm
    confirm(text: string, danger: boolean = false, title: string = '¿Confirmación?'): Promise<boolean> {
        return new Promise((res) => {
            swal({
                title: title,
                text: text,
                icon: 'warning',
                buttons: ['Cancelar', 'Ok'],
                dangerMode: danger
            }).then((ok) => {
                if (ok) {
                    res(true);
                } else {
                    res(false);
                }
            });
        });
    }
}
