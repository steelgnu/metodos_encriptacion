using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Seg_Inf
{
	class MainClass
	{
		public void leerASCII() {
			List<string> cadenaCompleta = new List<string>();
			try
			{ 
				Console.WriteLine("Inserta la direccion del archivo .txt");
				string direc = Console.ReadLine();
				using (StreamReader sr = new StreamReader(direc, false))
				{
					string linea;
					String temporal2 = "";

					while ((linea = sr.ReadLine())!=null) {
						linea = linea + " ";
						string temporal = "";
						for (int i = 0; i < linea.Length; i++)
						{
							if (linea[i] != ' ')
							{
								temporal = temporal + linea[i];
							}
							else
							{
								int numeroTemporal = Int32.Parse(temporal);
								temporal = Convert.ToChar(numeroTemporal).ToString();                                
								temporal2 = temporal2 + temporal;
								System.Console.Write(temporal);
								temporal = "";
							}
						}
						cadenaCompleta.Add(temporal2);
						temporal2 = "";
						string resul="";

						//using (System.IO.StreamWriter file = new StreamWriter(direc))
							for (int i = 0; i < cadenaCompleta.Count;i++ ) 

							{
								resul +=cadenaCompleta[i]+" ";
						//		file.WriteLine(cadenaCompleta[i]+" ");
							}
							Console.WriteLine ("\nIngrese la ruta donde desea guardar su texto cifrado ---> ");
							string fic = Console.ReadLine ();
							System.IO.StreamWriter sw = new System.IO.StreamWriter(fic);
							sw.WriteLine(resul);
						    sw.Close();

						
					} 
				}
			}
			catch (Exception)
			{
				Console.WriteLine("El archivo no se puede leer ahora");
			}
			Console.ReadKey();
		}
		/**Metodo para leer el texto */
		public void leerTexto()
		{
			int codigo=0;
			String codigoString = "";
			List<string> cadenaCompleta = new List<string>();
			try
			{
				Console.WriteLine("Inserta la direccion del archivo .txt");
				string direc = Console.ReadLine();
				using (StreamReader sr = new StreamReader(direc, false))
				{
					string linea;

					while ((linea = sr.ReadLine()) != null)
					{
						for (int i = 0; i < linea.Length;i++ ) { 
							char temporal = linea[i];
							codigo = (int)temporal;
							codigoString = codigoString + codigo.ToString();
							System.Console.Write(codigo+ " ");
						}
						cadenaCompleta.Add(codigoString+ " ");
						codigoString = "";
					}
					System.Console.WriteLine("");
					string resul="";
					//using (System.IO.StreamWriter file = new StreamWriter(direc))
						for (int i = 0; i < cadenaCompleta.Count; i++)
					    {

							resul += cadenaCompleta[i]+" ";
					//		file.WriteLine(cadenaCompleta[i]);
						}//end of for
					Console.WriteLine ("\nIngrese la ruta donde desea guardar su texto cifrado ---> ");
					string fic = Console.ReadLine ();
					System.IO.StreamWriter sw = new System.IO.StreamWriter(fic);
					sw.WriteLine(resul);
					sw.Close();
				}
			}
			catch (Exception)
			{
				Console.WriteLine("El archivo no se puede leer");
			}
			Console.ReadKey();
		}

		/**definimos el arreglo de letras mayusculas y minusculas, el cual nos sirve para el metodo de cesar*/
		string[] mayu = {"A","B","C","D","E","F","G","H","I","J","K","L","M","N","Ñ","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
		string[] minu = {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","ñ","o","p","q","r","s","t","u","v","w","x","y","z"};

		/**metodo para cargar el archivo txt del metodo cesar*/
		public string cargarArchivo(string path){

			string filename = path;
			string texto = "";
			FileStream stream = new FileStream (filename, FileMode.Open, FileAccess.Read);
			StreamReader reader = new StreamReader (stream);
			while (reader.Peek() > -1) {
				texto+=reader.ReadLine()  + "\n";
			}
			/**cierre del documento*/
			reader.Close();
			/**retornamos el texto*/
			return texto;
		}//end of cargarArchivo

		/**metodo para obtener la posicion de una letra en el arreglo de mayusculas*/
		public int posicionMayu (string c){
			for(int i = 0; i <mayu.Length; i++){
				if (mayu [i] == c) {
					return i;
				}//end of if
			}//end of ofr
			return -1;
		}//end of posicion mayusculas

		/**metodo para obtenrr la posicion de una letra en el arreglo de minusculas*/
		public int posicionMinu (string c){
			for(int i = 0; i <minu.Length; i++){
				if (minu[i] == c) {
					return i;
				}//end of if
			}//end of ofr
			return -1;
		}//end of posicion minusculas

		/**algoritmo de encriptacion cesar*/
		private string codificando(char l, int n){
			string codifi = "";
			/**si la letra leida es minuscula*/
			if (char.IsLower (l)) {
				int pos = posicionMinu (l.ToString ());
				if (pos >= 0) {
					int pos2 = (pos + n) % minu.Length;
					codifi = minu [pos2];
				} else {
					codifi = l.ToString ();
				}

			}
			/**si es una letra mayuscula*/
			if (char.IsUpper (l)) {
				int pos = posicionMayu (l.ToString ());

				if
					(pos >= 0)
				{
					int pos2 = (pos+ n) % mayu.Length;
					codifi = mayu[pos2];
				}
				else codifi = l.ToString();
			}
			/**si es un espcio en blanco*/
			if(char.IsWhiteSpace(l))
				codifi = " ";
			//Si es un digito
			if(char.IsDigit(l))
				codifi = l.ToString();
			/**Si es un signo de puntuacion*/
			if(char.IsPunctuation(l))
				codifi = l.ToString();
			if(char.IsControl(l))
				codifi = l.ToString();
			return codifi;
		}//end of codificando


		/**metodo para decodificar cesar*/

		private string decodificando(char l, int n){
			string codifi = "";
			//si la letra leida es minuscula
			if (char.IsLower (l)) {
				int pos = posicionMinu (l.ToString ());
				if (pos >= 0) {
					int pos2 = (pos - n) % minu.Length;
					codifi = minu [pos2];
				} else {
					codifi = l.ToString ();
				}

			}
			/**si es una letra mayuscula*/
			if (char.IsUpper (l)) {
				int pos = posicionMayu (l.ToString ());

				if
					(pos >= 0)
				{
					int pos2 = (pos - n) % mayu.Length;
					codifi = mayu[pos2];
				}
				else codifi = l.ToString();
			}
			/**si es un espcio en blanco*/
			if(char.IsWhiteSpace(l))
				codifi = " ";
			/**Si es un digito*/
			if(char.IsDigit(l))
				codifi = l.ToString();
			/**Si es un signo de puntuacion*/
			if(char.IsPunctuation(l))
				codifi = l.ToString();
			if(char.IsControl(l))
				codifi = l.ToString();
			return codifi;
		}//end of decodificando


		public string encriptaCesar(string texto, int desplazamiento)
		{
			int n = Convert.ToInt32(desplazamiento);
			string input = texto;
			string txCifrado = "";
			foreach
				(char c in input)
			{
				txCifrado += codificando(c, n);
			}
			return txCifrado;
		}//end of encriptar


		public string desencriptaCesar(string texto, int desplazamiento)
		{
			int n = Convert.ToInt32(desplazamiento);
			string input = texto;
			string txdesCifrado = "";
			foreach
				(char c in input)
			{
				txdesCifrado += decodificando(c, n);
			}
			return txdesCifrado;
		}//end of desencriptar

		/**Menu principal del programa(menu concentrado)*/
		public static void Main (string[] args)
		{   
			int opi = 0, o=0;
			Console.WriteLine ("\t BIENVENIDO A 4 METODOS DE ENCRIPTACION\n");
			do{
				Console.WriteLine(">> Seleeciona una opcion:");
				Console.WriteLine("1. Cesar ");
				Console.WriteLine("2. Morse");
				Console.WriteLine("3. ASCII");
				Console.WriteLine("4. Escitala");
				Console.WriteLine("5. Salir");

				o=Int32.Parse(Console.ReadLine());
				switch(o){
				  case 1: /**Codigo Cesar*/
					Console.WriteLine ("Bienvenido, con este programa puede encriptar o desencriptar textos con el metodo cesar\n\n");
					/**instancia*/
					MainClass cesar = new MainClass ();
					/**menu de opciones*/
					int op = 1,res;
					while (op != 3) {
						Console.WriteLine ("Menu de opciones del cifrado César\n1 = Cifrar\n2 = Descifrar\n3 = Salir del programa\n");
						res = Convert.ToInt32 (Console.ReadLine());
						op = res;
						if (res == 1) {
							Console.WriteLine ("Ingrese la ruta del archivo");
							string ruta = Console.ReadLine ();
							string texto = cesar.cargarArchivo (ruta);
							Console.WriteLine ("Texto original normal ---> \n");
							Console.WriteLine (texto);
							Console.WriteLine ("Ingrese el numero de desplazamiento que desea para su cifrado ---> ");
							int desplaza = Convert.ToInt32 (Console.ReadLine());
							string encriptado = cesar.encriptaCesar (texto,desplaza);
							Console.WriteLine ("El texto encriptado es el siguiente\n\n\n"+encriptado);
							Console.WriteLine ("\nIngrese la ruta donde desea guardar su texto cifrado ---> ");
							string fic = Console.ReadLine ();
							System.IO.StreamWriter sw = new System.IO.StreamWriter(fic);
							sw.WriteLine(encriptado);
							sw.Close();
						}
						if(res == 2){
						  
							Console.WriteLine ("Ingrese la ruta del archivo");
							string ruta = Console.ReadLine ();
							string texto = cesar.cargarArchivo (ruta);
							Console.WriteLine ("Texto original normal ---> \n");
							Console.WriteLine (texto);
							Console.WriteLine ("Ingrese el numero de desplazamiento que desea para su descifrado ---> ");
							int desplaza = Convert.ToInt32 (Console.ReadLine());
							string desencriptado = cesar.desencriptaCesar (texto,desplaza);
							Console.WriteLine ("El texto desencriptado es el siguiente\n\n\n"+desencriptado);
							Console.WriteLine ("\nIngrese la ruta donde desea guardar su texto descifrado ---> ");
							string fic = Console.ReadLine ();
							System.IO.StreamWriter sw = new System.IO.StreamWriter(fic);
							sw.WriteLine(desencriptado);
							sw.Close();
						}
						if (res == 3) {
							Console.WriteLine ("\nPrograma finalizado exitosamente\n");
							Console.In.Close();
						}//end of if
					}//end of while

				  break;
				  case 2: /**Codigo Morse*/
					int ka=1;
					while(ka == 1){
					Console.WriteLine(" --- CODIGO MORSE --- ");
					char []letras = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
					string [] Morse = { ".-" , "-..." , "-.-." , "-.." , "." , "..-." , "--." , "...." , ".." , ".---" , "-.-" , ".-.." , "--" , "-." , "---" , ".--." , "--.-" ,  ".-." , "..." , "-" , "..-" , "...-" , ".--" , "-..-" , "-.--" , "--.." , "|" };
					int opcion;
					string direccion,opc;
					Console.WriteLine("Porfavor selecciona: \n 1. Morse a Texto \n 2. Texto a Morse \n 3. Salir");
					opc=Console.ReadLine();
					opcion = Int32.Parse (opc);

					if (opcion == 1) { /**Morse - Texto*/
						try{
							/**solicita la direccion del archivo*/
							Console.WriteLine("Inserta la direccion de tu archivo .txt");
							direccion = Console.ReadLine (); /**recibe la direccion*/
							StreamReader lec = new StreamReader(direccion); 
							string lineas = lec.ReadLine();
							string r=null;
							while(lineas != null){
								char [] delimitador = {' '};/**cada palabra será limitada por espacios*/
								string []palabras= lineas.Split(delimitador);/**Split separar las palabras y las agrega al arreglo palabras*/
								for(int j=0; j < palabras.Length;j++){
									string  l = palabras[j];
									string [] caracter = l.Split(' ');
									for(int h = 0;h < caracter.Length;h++){
										for(int i = 0;i < Morse.Length;i++){
											if(caracter[h].Equals(Morse[i])){
												r=r+letras[i];
											}//end of if

										}//end of for i
											r+=" ";
									
									}//end of for h
									Console.WriteLine(" ");
								}//end of palabra*/
								r=r+" ";
								lineas=lec.ReadLine();
							}//end of while
							lec.Close();
							Console.WriteLine ("\nIngrese la ruta donde desea guardar su texto descifrado ---> ");
							string fic = Console.ReadLine ();
							System.IO.StreamWriter sw = new System.IO.StreamWriter(fic);
							sw.WriteLine(r);
								sw.Close();

						}//end of try
						catch(Exception e)
						{
							Console.WriteLine ("Exception"+e.Message);
						}//end of catch
						finally{
							Console.WriteLine ("Lectura Terminada");
						}//end of finally


					}//end of if

					else if (opcion == 2) {//Texto - Morse

						try{
							/**solicita la direccion del archivo*/
							Console.WriteLine("Inserta la direccion de tu archivo .txt");
							direccion = Console.ReadLine (); //recibe la direccion
							StreamReader lec = new StreamReader(direccion); 
							string lineas = lec.ReadLine();


							string r=null;
							int i = 0;
							while(lineas != null){
								lineas.ToLower();
								char [] delimitador = {' '};/**cada palabra será limitada por espacios*/
								string []palabras= lineas.Split(delimitador);/**Split separar las palabras y las agrega al arreglo palabras*/
								for(int x = 0;x < palabras.Length;x++){
									string letr = palabras[x];
									for(int y=0;y<letr.Length;y++){
										for(int w =0; w<Morse.Length;w++){
											if(letr[y].Equals(letras[w])){
												r=r+Morse[w]+" ";
											}//end of if
										}
									}
								}

								r=r+" ";
								lineas=lec.ReadLine();
								i++;
							}//end of while
							lec.Close();
							/**sobreescribe en el archivo original*/
							Console.WriteLine(r);
							Console.WriteLine("La información fue agregada con exito a su archivo original\n"+r);
							StreamWriter escribir  = new StreamWriter(direccion);
							escribir.WriteLine(r);
							escribir.Close(); 

						}//end of try
						catch(Exception e)
						{
							Console.WriteLine ("Exception"+e.Message);
						}//end of catch
						finally{
							Console.WriteLine ("Lectura Terminada");
						}//end of finally

					}//end of elseif
					else if(opcion ==3) {
							break;
					}//end of else
				      Console.WriteLine("\n -- A que menu desea regresar? \n1. Principal \n2. Morse");
						ka=Int32.Parse(Console.ReadLine());
					}//end of while 
				  break;
				  case 3:/**metodo ascii*/
					int d=1;
					while(d==1){
					int cii;
					Console.WriteLine(" ---  ASCII  ---");
					Console.WriteLine(" - Selecciona una opcion: \n 1. Texto a ASCII \n 2. ASCII a Texto \n 3. Salir");
					cii = Int32.Parse(Console.ReadLine());
					MainClass ascii = new MainClass();
					if(cii == 1){
						ascii.leerTexto();
					}//end of if
					else if(cii == 2){
						ascii.leerASCII();
					}
					else{
							break;
					}
						Console.WriteLine("A qué menú deseas regresar?? \n1. ASCII \n2. Principal");
						d=Int32.Parse(Console.ReadLine());
					}
					break;
				case 4:/**escitala*/
					int fe = 1;
					do{
						int x=0, y=0;
						char[,] esci;
						int opcion = 0;
						string linea="",direccion;
						Console.WriteLine(" ---> ESCITALA <---");
						Console.WriteLine ("Escribe la altura:");
						x = Int32.Parse (Console.ReadLine());
						Console.WriteLine ("Escribe ancho:");
						y = Int32.Parse (Console.ReadLine());
						esci = new char[x,y];

						Console.WriteLine ("\nSelecciona una opcion: \n1. Cifrar \n 2. Descifrar");
						opcion = Int32.Parse (Console.ReadLine());
						if(opcion==1){
							try{
								/**solicita la direccion del archivo*/
								Console.WriteLine("\nInserta la direccion de tu archivo .txt");
								direccion = Console.ReadLine (); //recibe la direccion
								StreamReader lec = new StreamReader(direccion); 
								string lineas = lec.ReadLine();

								string r=null;
								while(lineas != null){
									lineas.ToLower();
									char [] delimitador = {' '};/**cada palabra será limitada por espacios*/
									string []palabras= lineas.Split(delimitador);/**Split separar las palabras y las agrega al arreglo palabras*/
									for(int i = 0;i < palabras.Length ;i++){
										string letr = palabras[i];
										for(int j=0;j<letr.Length;j++){
											r+= letr[j];
										}
										r=r+" ";
									}

									lineas=lec.ReadLine();
								}//end of while
								Console.WriteLine ();
								/**se obtiene la frase del txt*/
								Console.WriteLine (r);
								lec.Close();

								int contador = 0;

								Console.WriteLine("Tamaño frase: "+r.Length);
								Console.WriteLine("Alto: "+x+" Ancho: "+y);

								for(int i = 0; i < x ; i++){
									for(int j =0; j < y; j++){
										if (contador >= r.Length) {
											esci[i,j] = ' ';
										} else {
											esci[i,j] = r[contador];
										}//end of else
										contador++;
									}//end of for j
								}//end of for i

								for(int i=0; i<y;i++){
									for(int j=0;j<x;j++){
										linea += esci [j, i];
									}
								}
								Console.WriteLine ();
								/**resultado de la encriptar*/
								Console.WriteLine(linea);

								/**guardar en archivo de texto*/
								Console.WriteLine ("\nIngrese la ruta donde desea guardar su texto cifrado ---> ");
								string fic = Console.ReadLine ();
								System.IO.StreamWriter sw = new System.IO.StreamWriter(fic);
								sw.WriteLine(linea);
								sw.Close();

							}//end of try
							catch(Exception e)
							{
								Console.WriteLine ("Exception"+e.Message);
							}//end of catch
							finally{
								Console.WriteLine ("Lectura Terminada");
							}//end of finally


						}//end of if texto-escitala
						else if(opcion == 2){
							try{
								/**solicita la direccion del archivo*/
								Console.WriteLine("Inserta la direccion de tu archivo .txt");
								/**recibe la direccion*/
								direccion = Console.ReadLine (); 
								StreamReader lec = new StreamReader(direccion); 
								string lineas = lec.ReadLine();

								string r=null;
								while(lineas != null){
									lineas.ToLower();
									/**cada palabra será limitada por espacios*/
									char [] delimitador = {' '};
									/**Split separar las palabras y las agrega al arreglo palabras*/
									string []palabras= lineas.Split(delimitador);
									for(int i = 0;i < palabras.Length ;i++){
										string letr = palabras[i];
										for(int j=0;j<letr.Length;j++){
											r+= letr[j];
										}
										r=r+" ";
									}

									lineas=lec.ReadLine();
								}//end of while
								Console.WriteLine ();
								/**se obtiene la frase del txt*/
								Console.WriteLine (r);
								lec.Close();

								int contador = 0;
								for(int i = 0; i < y ; i++){
									for(int j =0; j < x; j++){
										esci[j,i] = r[contador];
										contador++;
									}//end of for j
								}//end of for i

								for(int i=0; i<x;i++){
									for(int j=0;j<y;j++){
										linea += esci [i, j];
									}
								}
								Console.WriteLine ();
								/**resultado de la encriptacion*/
								Console.WriteLine(linea);

								/**guardar en archivo de texto*/
								Console.WriteLine ("\nIngrese la ruta donde desea guardar su texto cifrado ---> ");
								string fic = Console.ReadLine ();
								System.IO.StreamWriter sw = new System.IO.StreamWriter(fic);
								sw.WriteLine(linea);
								sw.Close();

							}//end of try
							catch(Exception e)
							{
								Console.WriteLine ("Exception"+e.Message);
							}//end of catch
							finally{
								Console.WriteLine ("\nLectura Terminada");
							}//end of finally

						}
						Console.WriteLine("\n A que menu desea regresar \n 1.Escitala \n 2.Menu Principal ");
						fe=Int32.Parse(Console.ReadLine());
					}while(fe==1);//end of while
					 
					break;
				case 5:
					Environment.Exit(0);
					break;
				}//end of switch

				Console.WriteLine("Qué desea hacer??  \n 1. Ir menú principal  \t2.Salir");
				opi=Int32.Parse(Console.ReadLine());

			}while(opi == 1);
		}
	}
}
