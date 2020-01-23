# ModularBuildings
Project description and source code will be updated soon

## Implementation
1. Block Data Structure The Current system takes several types of .fbx files. All of the .fbx files are currently a block with dimension (LxWxH) 4x4x3.5 meter. Symbols used to represent the shape from Top View (Red Dot represents pivot)

2. ModularRules Generator: Before placing each block, the building structure is designed from different rules to place them correctly. The rules are set from ModularRulesGenerator class.  The class places window blocks everywhere in a 3D grid. Then it creates the Gap in it from the 2D footprint array and fits in the height curve function to create the facade shape from the front direction. Then it detects the corner and places the corner blocks. Lastly, it processes the roof and ground floor. After each process, the rules get re-written in the 3D string array. 

![1_Shape.pngLogo](/1_Shape.png)

3.  Modular Building System: The building systems contains the data structure of the physical blocks (.fbx files). From the rules created by ModularRulesGenerator, it instantiates the blocks. Each block has a BuildingBlock class attached to it which contains their dimension. For the moment the dimensions are known and hardcoded. Later on, when we will be completed with the BlockMaker task (B3), will be able to measure it from the code. By calculating the dimension and distance, the physical blocks are placed. Later on, the post-processing of the step 2 ia done. 

4. The building creation tool takes the following inputs from the artist 1. Length (In blocks) 2. Height (In blocks) 3. Depth (In blocks) 4. The shape of Facade Height (In curve) 5. Building Footprint