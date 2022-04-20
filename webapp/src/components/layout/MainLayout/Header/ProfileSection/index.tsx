import React, { useRef } from 'react'
import { useTheme } from '@mui/material/styles'
import Avatar from '@mui/material/Avatar'
import Chip from '@mui/material/Chip'
import CurrencyExchangeIcon from '@mui/icons-material/CurrencyExchange'
// import Logo from '../../../common/Logo'
// import User1 from 'assets/images/users/user-round.svg'

const ProfileSection = () => {
  const theme = useTheme()
  const anchorRef = useRef(null)

  const handleToggle = () => {
    //setOpen((prevOpen) => !prevOpen);
  }
  const tag = <CurrencyExchangeIcon color={'secondary'} />
  return (
    <>
      <Chip
        sx={{
          height: '48px',
          alignItems: 'center',
          borderRadius: '27px',
          transition: 'all .2s ease-in-out',
          borderColor: theme.palette.success.light,
          backgroundColor: theme.palette.success.light,
          '&[aria-controls="menu-list-grow"], &:hover': {
            borderColor: theme.palette.success.main,
            background: `${theme.palette.success.main}!important`,
            color: theme.palette.success.light,
            '& svg': {
              stroke: theme.palette.success.light,
            },
          },
          '& .MuiChip-label': {
            lineHeight: 0,
          },
        }}
        icon={
          <Avatar
            // src={User1}
            sx={{
              margin: '8px 0 8px 8px !important',
              cursor: 'pointer',
            }}
            ref={anchorRef}
            aria-controls={'menu-list-grow'}
            aria-haspopup="true"
            color="inherit"
          />
        }
        //<Settings stroke={1.5} size="1.5rem" color={"primary"} />
        label={<>{tag}</>}
        variant="outlined"
        ref={anchorRef}
        aria-controls={'menu-list-grow'}
        aria-haspopup="true"
        onClick={handleToggle}
        color="primary"
      />
    </>
  )
}

export default ProfileSection
