export abstract class ServiceParent {

    constructor() {
    }

    logHandled(message: any) {
        console.log('Service:', message);
    }

    errorHandled(message: any) {
        console.error('Service: ', message);
    }

}