import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class LoggerService {
    logInfo(message: string) {
        console.log(message);
    }

    logWarning(message: string) {
        console.warn(message);
    }

    logError(error: any) {
        console.error(error);
    }
}