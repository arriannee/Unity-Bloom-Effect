### Modelación y Computación Gráfica para Ingenieros - Otoño 2024
# Proyecto Independiente: Bloom

## Descripción
En esta tarea se implementó el efecto Bloom, el cual añade un brillo característico alrededor de los objetos brillantes, creando un resplandor que simula la luz dispersándose en una lente. Se probaron las siguientes técnicas/herramientas:

- Uso de OpenGL: Inicialmente se intentó implementar el efecto utilizando OpenGL, siguiendo este [tutorial](https://learnopengl.com/Advanced-Lighting/Bloom).
- Cambio a Unity: Por motivos de tiempo, se decidió cambiar a Unity y se intentó simular el efecto Bloom de dos maneras:
Utilizando dos shaders: glow y blur.
Ajustando la propiedad de emisión de los materiales.

## Instrucciones para Abrir el Proyecto
El proyecto final se realizó en Unity, por lo que se necesita tener Unity instalado para abrirlo. Sigue estos pasos:

1. Abre Unity.
2. Selecciona "Open Project" y navega hasta la carpeta My project. (¡Toma un poco de tiempo en que cargue! Pues he eliminado la carpeta Library para alivianar la entrega).
En caso de abrir con una escena vacía, por favor, dirigirse a Assets/Scenes donde se encontrará la escena ```LluviaDeEstrellas```.
3. Una vez abierto el proyecto, simplemente presione el botón de "Play" o use el atajo de teclado Ctrl + P. 
4. Al ejecutar el proyecto, se debería ver una estrella en pantalla a la cual se le aplicó el efecto Bloom utilizando shaders. Las estrellas siguientes tienen la emisión alterada, la cual se atenúa si no han colisionado en un tiempo.

## Estructura del Proyecto
La estructura del proyecto es la siguiente:

```
My project
├── Assets
│   ├── BlurMaterial.mat
│   ├── BlurShader.shader
│   ├── Estrella.prefab
│   ├── EstrellaGenerador.cs
│   ├── EstrellaMat.mat
│   ├── GlowMaterial.mat
│   ├── GlowShader.shader
│   ├── ReboteMat.physicMaterial
├── Logs
├── Packages
├── ProjectSettings
├── UserSettings
├── .vsconfig
├── Assembly-CSharp.csproj
├── Assembly-CSharp-Editor.csproj
├── My project.sln
├── UpgradeLog
```

## Comparación con Bloom Convencional
Para comparar este efecto con el Bloom convencional integrado en Unity, en el editor se incluye un ```PostProcessingVolume``` con un efecto Bloom desactivado. Se puede activar y desactivar para ver las diferencias.

## Fuentes de Investigación
Durante la investigación de este efecto, se utilizaron las siguientes fuentes:
- [Efficient Gaussian Blur with linear sampling](https://learnopengl.com/Advanced-Lighting/Bloom): Describe el desenfoque gaussiano muy bien y cómo mejorar su rendimiento utilizando el muestreo de textura bilineal de OpenGL.
- [getIntoGameDev Bloom](https://github.com/amengede/getIntoGameDev/blob/main/pyopengl%202022/14%20-%20Bloom/finished/finished.py): Repositorio sobre cómo mejorar el efecto Bloom combinando múltiples curvas gaussianas para sus pesos.
- [How to do good Bloom for HDR rendering](https://docs.unity3d.com/560/Documentation/Manual/PostProcessing-Bloom.html): Artículo de Kalogirou que describe cómo mejorar el efecto Bloom utilizando un mejor método de desenfoque gaussiano.
- [Bloom (shader effect)](https://en.wikipedia.org/wiki/Bloom_(shader_effect)): Página de Wikipedia que proporciona una visión general del efecto Bloom en los shaders.

Personalmente, fue muy entretenido investigar y aprender sobre este efecto. A pesar de qué no es la mejor implemetación, siento que se pudo simular parcialmente, ¡espero que sea bonito de ver!

 Espero avanzar y seguir agregándole cosas que me gusten. ⋆｡°✩

