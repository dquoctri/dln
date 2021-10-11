import { useDispatch } from 'react-redux'
import { bindActionCreators } from 'redux'
import * as actions from '../stote/actions'

export const useActions = () => {
  const dispatch = useDispatch()
  return bindActionCreators(actions, dispatch)
}
