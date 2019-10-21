using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using ICoordinateTransformation = ProjNet.CoordinateSystems.Transformations.ICoordinateTransformation;

namespace Eflatun.GeoUnity.Projection
{
    /// <summary>
    /// Performs forward and inverse projections between two coordinate systems.
    /// </summary>
    public class Projector
    {
        private readonly ICoordinateTransformation _forwardTransform;
        private readonly ICoordinateTransformation _inverseTransform;

        public Projector(CoordinateSystem fromCs, CoordinateSystem toCs)
        {
            CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();
            _forwardTransform = ctfac.CreateFromCoordinateSystems(fromCs, toCs);
            _inverseTransform = ctfac.CreateFromCoordinateSystems(toCs, fromCs);
        }

        public double[] Project(double[] @from)
        {
            return _forwardTransform.MathTransform.Transform(@from);
        }

        public double[] InverseProject(double[] @from)
        {
            return _inverseTransform.MathTransform.Transform(@from);
        }
    }
}
