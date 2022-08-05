interface PageProps {
  title?: string
}

export interface CustomPageProps extends PageProps {
  description?: string
  children: any
}

export default PageProps
