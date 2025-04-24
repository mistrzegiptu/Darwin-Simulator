# Darwin World Simulation  

## Overview  
Darwin World is an evolutionary biology simulation where virtual creatures (animals) live in a dynamic environment of steppes and jungles. The animals move, eat plants, reproduce, and evolve based on genetic traits, allowing users to observe natural selection and mutations over time.  

Built in **C# with WPF**, the project features a graphical interface for configuring and visualizing the simulation.  

---

## Key Features  
- **Dynamic Map**: Grid-based world with steppes (sparse plants) and jungles (dense plants).  
- **Animal Behavior**:  
  - Genes determine movement (8 possible directions).  
  - Energy decreases daily; eating plants restores energy.  
  - Reproduction when energy is sufficient, with genetic crossover and mutations.  
- **Configurable Rules**: Adjust map size, plant growth, mutations, and more.  
- **Statistics**: Track population, lifespans, dominant genotypes, and lineage.  
- **Visualization**: Real-time animation with selectable animals and highlighted traits.  

---

## Simulation Workflow  
1. **Daily Cycle**:  
   - Remove dead animals.  
   - Move animals based on their genes.  
   - Resolve plant consumption.  
   - Trigger reproduction for high-energy pairs.  
   - Grow new plants.  
2. **Environmental Variants**:  
   - *Globe* (wrapping edges), *Poles* (energy loss near edges), *Fires* (spreading hazards).  
3. **Mutation Types**:  
   - Random changes, slight adjustments (`±1`), or gene swaps.  

---

## Screenshots & Demos  
| Feature          | Preview |  
|------------------|---------|  
| **Simulation**   | ![Simulation GIF](demo.gif) |  
| **Configuration**| ![Config Panel](config.png) |  
| **Statistics**   | ![Stats View](stats.png) |  

---

## Technical Details  
- **Language**: C#  
- **Framework**: WPF (Windows Presentation Foundation)  
- **Build**: Gradle  
- **Features**:  
  - Multiple concurrent simulations.  
  - Pause/resume controls.  
  - Animal tracking (genome, energy, offspring).  
  - CSV export for stats.  

---

## How to Run  
1. Clone the repository.  
2. Build with Gradle: `gradlew build`  
3. Launch: `DarwinWorld.exe`  
4. Configure settings and start simulating!  

---

## Implemented Variants  
- **Map**: [Your variant, e.g., *Poles* or *Fires*]  
- **Animals**: [Your variant, e.g., *A Bit of Madness*]  

---

## License  
Educational project inspired by:  
- *Land of Lisp* (Conrad Barski)  
- *Genetic Algorithms and Their Applications* (David E. Goldberg).  