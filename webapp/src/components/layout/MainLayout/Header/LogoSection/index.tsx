import { Link } from 'react-router-dom'

import Logo from '../../../common/Logo'

const LogoSection = () => {
  return (
    <Link to="/" style={{ textDecoration: 'none' }}>
      <button>
        <Logo />
        <h5>Deadl</h5>
        <h5>!</h5>
        <h5>ne</h5>
      </button>
    </Link>
  )
}

export default LogoSection
