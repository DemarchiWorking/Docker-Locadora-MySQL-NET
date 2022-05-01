import React from 'react'

import { Link } from 'react-router-dom'

import './headerMain.css'

function HeaderMain() {
    return (

        <header>
            <div className="container">
                <div className="logo" >
                    <Link to="/"><h1>Locadora de Filmes</h1></Link>
                </div>

                <div className="btn-newPost" >

                    <Link to="/funcionalidades" >
                        <button> Funcionalidades </button>
                    </Link>

                </div>
            </div>
        </header>

    )
}

export default HeaderMain