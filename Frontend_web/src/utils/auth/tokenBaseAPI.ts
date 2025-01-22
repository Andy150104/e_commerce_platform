import { useTokenClientAxios } from "./authHttp"

export class TokenBaseAPI {
    resource: string

    constructor(resource: string){
        this.resource = resource
    }
    
    protected async invoke({type = 'get', path = '', body =null}){
        const tokenClient = useTokenClientAxios()
        const { data }: { data: any} = await (tokenClient as any)[type](
            this.resource + path,
            body
        ) 
        return data
    }

    async get(path = ''){
        return await this.invoke({ path })
    }

    async post(body: any, path = ''){
        return await this.invoke({ type: 'post', body, path})
    }
}