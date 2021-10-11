import { RouteItem } from './configuration.d'

export const useCreatePaths = (routes: Array<RouteItem>): Array<string> => {
  const paths: Array<string> = []
  if (!routes) return paths
  routes.map(route => paths.push(route.path))
  return paths
}

