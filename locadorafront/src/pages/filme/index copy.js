import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Global from '../../../config/Global'

export function ListarCliente() {
    
    const [clientes, setClientes] = useState('a');

    useEffect(()=> {
        axios.get(Global.url+'cliente/listar')
            .then((response) => {    
                setClientes(response.data);

            })
            .catch((err)=>{                
                alert(Global.msgErro+ err);                
                console.log('errado');
            })
    }, []);

   
  return (
    <div className='cards'>      
        {clientes.map((post, key)=>{
          return (
              <div className='card'>
                          <header>
                            <h2>{post.title}</h2>
                          </header>

                          <p></p>

                          <div className='btns'>
                              <div className='btn-delete'>
                                  <button> Editar </button>
                              </div>
                              <div className='btn-delete'>
                                  <button> Editar </button>
                              </div>
                              <div className='btn-delete'>
                                  <button> Delete </button>
                              </div>
                          </div>
              </div>
          )
        })}

        
       test
    </div>
  );
}

  