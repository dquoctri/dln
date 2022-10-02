import { WebService } from './abstract-web.service'

export interface FileResult {
  filename: string
  data: Blob
}

export abstract class FileService extends WebService {
  constructor() {
    super()
  }

  protected async postDownload(url: string, payload?: unknown): Promise<FileResult> {
    const res = await this.instance.post(url, payload, { responseType: 'blob' })
    const filename = this.extractFilename(res.headers)
    return { filename, data: res.data }
  }

  protected async download(url: string): Promise<FileResult> {
    const res = await this.instance.get(url)
    const filename = this.extractFilename(res.headers)
    return { filename, data: res.data }
  }

  protected async downloadBlob(url: string): Promise<Blob> {
    const res = await this.instance.get(url, { responseType: 'blob' })
    return res.data
  }

  private extractFilename(headers: any): string {
    return headers['content-disposition']
      .split(';')
      .find((n: string) => n.includes('filename='))
      .match(/filename="(.*)"/)[1]
      .trim()
  }
}
