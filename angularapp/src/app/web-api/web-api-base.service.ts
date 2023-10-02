import { HttpClient, HttpHeaders, HttpParams, JsonpInterceptor } from "@angular/common/http";

export class WebApiServiceBase {
    constructor(protected httpClient: HttpClient, protected controllerName: string) {

    }

    get baseUrl(){
        let result: string = '';
        if(!window.location.origin){
            result = window.location.protocol + '//' + window.location.hostname + (window.location.port ? ':' + window.location.port : '');
        }
        else{
            result = window.location.origin + '/';
        }

        if(result.includes('localhost')){
            result = 'https://localhost:7193/'
        }
        return result;
    }

    private createHttpOptions(bodyAsJson?: string, params?: any, hasJsonResponse?: boolean): HttpOptions {
        const headers = this.createHttpHeaders(bodyAsJson, hasJsonResponse);
        const httpParams = this.createHttpParamsIfExsits(params);
        const httpOptions = new HttpOptions(headers, httpParams);
        return httpOptions;
    }

    private createHttpParamsIfExsits(params: any): HttpParams | undefined {
        if (params) {
            let httpParams = new HttpParams();
            for (const prop in params) {
                const propValue = params[prop];
                const propValueAsString = typeof (propValue) === 'string' ? propValue : JSON.stringify(propValue);
                httpParams = httpParams.set(prop, propValueAsString);
            }
            return httpParams;
        }
        return undefined;
    }

    private createHttpHeaders(bodyAsJson?: string, hasJsonResponse?: boolean): HttpHeaders {
        let headers = new HttpHeaders();
        if (bodyAsJson != '') headers = headers.append('Content-Type', 'application/json');
        if (hasJsonResponse) headers = headers.append('Accept', 'application/json');
        return headers;
    }

    protected async get<T>(methodName: string, params?: any, hasJsonResponse: boolean = true): Promise<T | undefined> {
        const httpOptions = this.createHttpOptions('', params, hasJsonResponse);
        return await this.httpClient.get<T>(`${this.baseUrl}${this.controllerName}/` + methodName, httpOptions).toPromise();
    }

    protected async post<T>(methodName: string, body?: any, hasJsonResponse: boolean = false): Promise<T | undefined> {
        const bodyAsJson = body ? JSON.stringify(body) : ''
        const httpOptions = this.createHttpOptions(bodyAsJson, null, hasJsonResponse);
        return this.httpClient.post<T>(`${this.baseUrl}${this.controllerName}/` + methodName, body, httpOptions).toPromise();
    }
}

export class HttpOptions {
    constructor(
        public headers?: HttpHeaders,
        public params?: HttpParams
    ) {
        this.observe = 'body';
        this.responseType = 'json';
    }
    public observe?: 'body';
    public reportProgress?: boolean;
    public responseType?: 'json';
    public withCredentials?: boolean;
}