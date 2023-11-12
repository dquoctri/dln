import React from "react"

const Footer = () => {
  return (
    <footer className="flex flex-col items-center bg-neutral-900 text-center text-white">
      {/* <!--Copyright section--> */}
      <div className="w-full p-4 text-center">
        © 2023 Copyright:
        <a className="text-neutral-800 dark:text-neutral-400" href="https://dqtri.com/">
          {" "}
          TW elements
        </a>
      </div>
    </footer>
  )
}

export default Footer
